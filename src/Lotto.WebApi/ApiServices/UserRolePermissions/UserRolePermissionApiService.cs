using AutoMapper;
using Lotto.Domain.Entities.Users;
using Lotto.Service.Configurations;
using Lotto.Service.Services.UserRolePermissions;
using Lotto.WebApi.Models.UserRolePermissions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.UserRolePermissions;

public class UserRolePermissionApiService(IUserRolePermissionService userRolePermissionService, IMapper mapper) : IUserRolePermissionApiService
{
    public async ValueTask<UserRolePermissionViewModel> CreateAsync(UserRolePermissionCreateModel createModel)
    {
        var createdUserRolePermission = await userRolePermissionService.CreateAsync(mapper.Map<UserRolePermission>(createModel));
        return mapper.Map<UserRolePermissionViewModel>(createdUserRolePermission);
    }

    public async ValueTask<UserRolePermissionViewModel> UpdateAsync(long id, UserRolePermissionUpdateModel updateModel)
    {
        var updatedUserRolePermission = await userRolePermissionService.UpdateAsync(id, mapper.Map<UserRolePermission>(updateModel));
        return mapper.Map<UserRolePermissionViewModel>(updatedUserRolePermission);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await userRolePermissionService.DeleteAsync(id);
    }

    public async ValueTask<UserRolePermissionViewModel> GetByIdAsync(long id)
    {
        var result = await userRolePermissionService.GetByIdAsync(id);
        return mapper.Map<UserRolePermissionViewModel>(result);
    }

    public async ValueTask<IEnumerable<UserRolePermissionViewModel>> GetAllAsync(
        PaginationParams @params,
        Filter filter)
    {
        var result = await userRolePermissionService.GetAllAsync(@params, filter);
        return mapper.Map<IEnumerable<UserRolePermissionViewModel>>(result);
    }

    public async ValueTask<IEnumerable<UserRolePermissionViewModel>> GetAlByRoleIdAsync(long roleId)
    {
        var result = await userRolePermissionService.GetAlByRoleIdAsync(roleId);
        return mapper.Map<IEnumerable<UserRolePermissionViewModel>>(result);
    }
}