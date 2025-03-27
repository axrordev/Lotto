using AutoMapper;
using Lotto.Domain.Entities.Users;
using Lotto.Service.Configurations;
using Lotto.Service.Services.UserRoles;
using Lotto.WebApi.Models.UserRoles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.UserRoles;

public class UserRoleApiService(IUserRoleService userRoleService, IMapper mapper) : IUserRoleApiService
{
    public async ValueTask<UserRoleViewModel> CreateAsync(UserRoleCreateModel createModel)
    {
        var createdUserRole = await userRoleService.CreateAsync(mapper.Map<UserRole>(createModel));
        return mapper.Map<UserRoleViewModel>(createdUserRole);
    }

    public async ValueTask<UserRoleViewModel> UpdateAsync(long id, UserRoleUpdateModel updateModel)
    {
        var updatedUserRole = await userRoleService.UpdateAsync(id, mapper.Map<UserRole>(updateModel));
        return mapper.Map<UserRoleViewModel>(updatedUserRole);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await userRoleService.DeleteAsync(id);
    }

    public async ValueTask<IEnumerable<UserRoleViewModel>> GetAllAsync(
        PaginationParams @params,
        Filter filter)
    {
        var result = await userRoleService.GetAllAsync(@params, filter);
        return mapper.Map<IEnumerable<UserRoleViewModel>>(result);
    }

    public async ValueTask<UserRoleViewModel> GetByIdAsync(long id)
    {
        var result = await userRoleService.GetByIdAsync(id);
        return mapper.Map<UserRoleViewModel>(result);
    }
}
