using Lotto.WebApi.Models.Permissions;
using Lotto.WebApi.Models.UserRoles;

namespace Lotto.WebApi.Models.UserRolePermissions;

public class UserRolePermissionViewModel
{
    public long Id { get; set; }
    public UserRoleViewModel UserRole { get; set; }
    public PermissionViewModel Permission { get; set; }
}
