using Lotto.Domain.Entities.Games;

namespace Lotto.Service.Services.Footballs;

public interface IFootballService
{
    ValueTask<Football> CreateAsync(Football football);
    ValueTask<PlayFootball> PlayAsync(PlayFootball playFootball);
    ValueTask<FootballResult> AddResultAsync(FootballResult footballResult);
    ValueTask AnnounceResultsAsync(long footballId);
    ValueTask<List<PlayFootball>> GetUserPlaysAsync(long userId);
}
