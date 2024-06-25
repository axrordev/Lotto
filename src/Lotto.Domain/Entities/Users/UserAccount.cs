using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities;

public class UserAccount : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public string CardNumber { get; set; }
    public decimal Balance { get; set; }
}
