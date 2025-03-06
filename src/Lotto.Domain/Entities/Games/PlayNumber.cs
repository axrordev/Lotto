using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities.Games;

public class PlayNumber : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long NumberId { get; set; }
    public Number Number { get; set; }
    public string SelectedNumbers { get; set; } // "5,12,23,34,45,6"
    public bool IsWinner { get; set; }
    public int AttemptsLeft { get; set; } = 1;
    public int AdsWatched { get; set; } = 0;
}
