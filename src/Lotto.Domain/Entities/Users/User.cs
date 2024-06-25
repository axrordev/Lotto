using Lotto.Domain.Entities.Advertisements;
using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities;

public class User : Auditable
{
    public string Username { get; set; }
    public long Phone { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long UserAccountId { get; set; }
    public UserAccount UserAccount { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
}
