
using Lotto.Service.Configurations;
using Lotto.WebApi.Models.UserRolePermissions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.UserRolePermissions;

public interface IUserRolePermissionApiService
{
    ValueTask<UserRolePermissionViewModel> CreateAsync(UserRolePermissionCreateModel createModel);
    ValueTask<UserRolePermissionViewModel> UpdateAsync(long id, UserRolePermissionUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserRolePermissionViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<UserRolePermissionViewModel>> GetAlByRoleIdAsync(long roleId);
    ValueTask<IEnumerable<UserRolePermissionViewModel>> GetAllAsync(
        PaginationParams @params,
        Filter filter);
}