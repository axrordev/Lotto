using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities.Games;

public class PlayFootball : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long FootballGameId { get; set; }
    public FootballGame FootballGame { get; set; }
    public TimeSpan GoalTime { get; set; }
    public bool IsWinner { get; set; }
}