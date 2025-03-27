

using Lotto.Service.Configurations;
using Lotto.WebApi.Models.Permissions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.Permissions;

public interface IPermissionApiService
{
    ValueTask<PermissionViewModel> CreateAsync(PermissionCreateModel createModel);
    ValueTask<PermissionViewModel> UpdateAsync(long id, PermissionUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<PermissionViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<PermissionViewModel>> GetAllAsync(PaginationParams @params, Filter filter);
}