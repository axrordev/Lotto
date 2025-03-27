
using Lotto.Service.Configurations;
using Lotto.WebApi.Models.UserRoles;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.UserRoles;

public interface IUserRoleApiService
{
    ValueTask<UserRoleViewModel> CreateAsync(UserRoleCreateModel createModel);
    ValueTask<UserRoleViewModel> UpdateAsync(long id, UserRoleUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserRoleViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<UserRoleViewModel>> GetAllAsync(
        PaginationParams @params,
        Filter filter);
}
