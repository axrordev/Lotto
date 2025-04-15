using Lotto.Data.Repositories;
using Lotto.Data.UnitOfWorks;
using Lotto.Domain.Entities.Games;
using Lotto.Domain.Entities;
using Lotto.Service.Exceptions;

namespace Lotto.Service.Services.Footballs;

public class FootballService : IFootballService
    {
        private readonly IRepository<Football> _footballRepository;
        private readonly IRepository<PlayFootball> _playFootballRepository;
        private readonly IRepository<FootballResult> _footballResultRepository;
        private readonly IRepository<Announcement> _announcementRepository;
        private readonly IUnitOfWork _unitOfWork;

        public FootballService(
            IRepository<Football> footballRepository,
            IRepository<PlayFootball> playFootballRepository,
            IRepository<FootballResult> footballResultRepository,
            IRepository<Announcement> announcementRepository,
            IUnitOfWork unitOfWork)
        {
            _footballRepository = footballRepository;
            _playFootballRepository = playFootballRepository;
            _footballResultRepository = footballResultRepository;
            _announcementRepository = announcementRepository;
            _unitOfWork = unitOfWork;
        }

        public async ValueTask<Football> CreateAsync(Football football)
        {
            try
            {
                if (string.IsNullOrEmpty(football.LigaName) || string.IsNullOrEmpty(football.HomeTeam) || string.IsNullOrEmpty(football.AwayTeam))
                    throw new ArgumentException("LigaName, HomeTeam, and AwayTeam are required.");

                if (football.MatchDay <= DateTime.UtcNow)
                    throw new ArgumentException("MatchDay must be in the future.");

                football.IsCompleted = false;
                var createdFootball = await _footballRepository.InsertAsync(football);
                await _unitOfWork.SaveAsync();

                var announcement = new Announcement
                {
                    Title = "New Football Game Created",
                    Message = $"Game {createdFootball.Id}: {football.HomeTeam} vs {football.AwayTeam}. Match Day: {football.MatchDay}.",
                    ExpiryDate = DateTime.UtcNow.AddDays(7),
                    IsActive = true
                };

                await _announcementRepository.InsertAsync(announcement);
                await _unitOfWork.SaveAsync();

                return createdFootball;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to create football game: {ex.Message}", ex);
            }
        }

        public async ValueTask<PlayFootball> PlayAsync(PlayFootball playFootball)
        {
            try
            {
                var football = await _footballRepository.SelectAsync(f => f.Id == playFootball.FootballId);
                if (football == null)
                    throw new NotFoundException("Football game not found.");

                if (DateTime.UtcNow > football.MatchDay)
                    throw new Exception("Football game is already completed. You cannot participate.");

                if (playFootball.GoalTime < 1 || playFootball.GoalTime > 90)
                    throw new ArgumentException("GoalTime must be between 1 and 90 minutes.");

                if (string.IsNullOrEmpty(playFootball.ScoringPlayer))
                    throw new ArgumentException("ScoringPlayer is required.");

                var createdPlayFootball = await _playFootballRepository.InsertAsync(playFootball);
                await _unitOfWork.SaveAsync();

                return createdPlayFootball;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to play football game: {ex.Message}", ex);
            }
        }

        public async ValueTask<FootballResult> AddResultAsync(FootballResult footballResult)
        {
            try
            {
                var football = await _footballRepository.SelectAsync(f => f.Id == footballResult.FootballId);
                if (football == null)
                    throw new NotFoundException("Football game not found.");

                if (DateTime.UtcNow < football.MatchDay)
                    throw new Exception("Football game is not yet completed. Please wait until the match day.");

                if (footballResult.HomeTeamScore < 0 || footballResult.AwayTeamScore < 0)
                    throw new ArgumentException("Team scores cannot be negative.");

                if (!footballResult.Goals.Any())
                    throw new ArgumentException("At least one goal detail is required.");

                foreach (var goal in footballResult.Goals)
                {
                    if (goal.GoalTime < 1 || goal.GoalTime > 90)
                        throw new ArgumentException($"Goal time {goal.GoalTime} must be between 1 and 90 minutes.");

                    if (string.IsNullOrEmpty(goal.ScoringPlayer))
                        throw new ArgumentException("Scoring player is required for each goal.");

                    if (goal.ScoringTeam != football.HomeTeam && goal.ScoringTeam != football.AwayTeam)
                        throw new ArgumentException($"Scoring team must be either {football.HomeTeam} or {football.AwayTeam}.");
                }

                var createdFootballResult = await _footballResultRepository.InsertAsync(footballResult);
                await _unitOfWork.SaveAsync();

                football.IsCompleted = true;
                await _footballRepository.UpdateAsync(football);
                await _unitOfWork.SaveAsync();

                return createdFootballResult;
            }
            catch (Exception ex)
            {
                throw new Exception($"Failed to add football result: {ex.Message}", ex);
            }
        }

        public async ValueTask AnnounceResultsAsync(long footballId)
        {
            try
            {
                var football = await _footballRepository.SelectAsync(f => f.Id == footballId);
                if (football == null)
                    throw new NotFoundException("Football game not found.");

                if (!football.IsCompleted)
                    throw new Exception("Football game is not completed yet.");

                var footballResult = await _footballResultRepository.SelectAsync(fr => fr.FootballId == footballId);
                if (footballResult == null)
                    throw new NotFoundException("Football result not found.");

                if (!footballResult.Goals.Any())
                    throw new Exception("No goals recorded in the football result.");

                var playFootballEntries = await _playFootballRepository.SelectAsEnumerableAsync(
                    pf => pf.FootballId == footballId);

                foreach (var playFootball in playFootballEntries)
                {
                    playFootball.IsWinner = footballResult.Goals.Any(goal =>
                        playFootball.GoalTime == goal.GoalTime &&
                        playFootball.ScoringPlayer == goal.ScoringPlayer);

                    await _playFootballRepository.UpdateAsync(playFootball);
                }

                await _unitOfWork.SaveAsync();

                var winners = playFootballEntries.Where(pf => pf.IsWinner).Select(pf => pf.UserId).ToList();
                var winnersMessage = winners.Any()
                    ? $"Winners: {string.Join(", ", winners)}"
                    : "No winners this time.";

                var goalsSummary = string.Join(", ", footballResult.Goals.Select(g =>
                    $"Goal at {g.GoalTime} minute by {g.ScoringPlayer} ({g.ScoringTeam})"));

                var announcement = new Announcement
                {
                    Title = $"Football Game {footballId} Results",
                    Message = $"Game Summary: {footballResult.HomeTeamScore}-{footballResult.AwayTeamScore}. Goals: {goalsSummary}. {winnersMessage}",
                    ExpiryDate = DateTime.UtcNow.AddDays(7),
                    IsActive = true
                };

                await _announcementRepository.InsertAsync(announcement);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error announcing football game results: {ex.Message}", ex);
            }
        }

        public async ValueTask<List<PlayFootball>> GetUserPlaysAsync(long userId)
        {
            try
            {
                var plays = await _playFootballRepository.SelectAsEnumerableAsync(
                    pf => pf.UserId == userId,
                    new[] { nameof(PlayFootball.Football) });

                return plays.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user plays: {ex.Message}", ex);
            }
        }
    }