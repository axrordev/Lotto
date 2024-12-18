using Lotto.Domain.Entities.Advertisements;
using Lotto.Domain.Entities.Games;
using Lotto.Domain.Entities.Users;
using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities;

public class User : Auditable
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long RoleId { get; set; }
    public UserRole Role { get; set; }
    public decimal Balance { get; set; }
    public bool IsBlocked { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public ICollection<PlayFootball> PlayFootballs { get; set; }
    public ICollection<PlayNumber> PlayNumbers { get; set; }
}
