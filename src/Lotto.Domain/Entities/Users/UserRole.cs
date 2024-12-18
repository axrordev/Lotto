using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities.Users;

public class UserRole : Auditable
{
    public string Name { get; set; }
}
