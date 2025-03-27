
using Lotto.Service.Services.UserRolePermissions;
using Lotto.Service.Services.UserRoles;

namespace Lotto.WebApi.Helpers;

public static class ServiceHelper
{
    public static IUserRoleService UserRoleService { get; set; }
    public static IUserRolePermissionService UserRolePermissionService { get; set; }
}
