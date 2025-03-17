using Lotto.Domain.Commons;

namespace Lotto.Domain.Entities.Users;

public class Permission : Auditable
{
    public string Controller { get; set; }
    public string Action { get; set; }
}