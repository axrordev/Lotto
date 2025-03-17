using Lotto.Domain.Commons;
using Lotto.Domain.Entities.Advertisements;
using Lotto.Domain.Entities.Games;

namespace Lotto.Domain.Entities.Users;

public class User : Auditable
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public long RoleId { get; set; }
    public UserRole Role { get; set; }
    public decimal Balance { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime? LastCommentTime { get; set; }
    public ICollection<PlayFootball> PlayFootballs { get; set; }
    public ICollection<PlayNumber> PlayNumbers { get; set; }
}
