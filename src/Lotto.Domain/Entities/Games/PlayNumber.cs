using Lotto.Domain.Commons;
using Lotto.Domain.Entities.Users;

namespace Lotto.Domain.Entities.Games;

public class PlayNumber : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long NumberId { get; set; }
    public Number Number { get; set; }
    public int[] SelectedNumbers { get; set; }
    public bool IsWinner { get; set; }
}
