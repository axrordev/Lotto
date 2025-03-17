using Lotto.Domain.Commons;
using Lotto.Domain.Entities.Users;

namespace Lotto.Domain.Entities.Games;

public class PlayFootball : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long FootballId { get; set; }
    public Football Football { get; set; }
    public int GoalTime { get; set; }
    public bool IsWinner { get; set; }
    public int AttemptsLeft { get; set; } = 1; 
    public int AdsWatched { get; set; } = 0;
}