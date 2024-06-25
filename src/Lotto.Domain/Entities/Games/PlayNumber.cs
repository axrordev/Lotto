using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities.Games;

public class PlayNumber : Auditable
{
    public long  UserId { get; set; }
    public User User { get; set; }
    public long NumberGameId { get; set; }
    public NumberGame NumberGame { get; set; }
    public List<int> SelectedNumbers { get; set; } = new List<int>();
    public bool IsWinner { get; set; }
}