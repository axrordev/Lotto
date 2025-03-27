
using Lotto.Service.Configurations;
using Lotto.WebApi.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.Users;

public interface IUserApiService
{
    ValueTask<UserViewModel> ModifyAsync(long id, UserUpdateModel updateModel);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<UserViewModel> GetAsync(long id);
    ValueTask<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter);
    ValueTask<UserViewModel> ChangePasswordAsync(string oldPasword, string newPassword, string confirmPassword);
    ValueTask<UserViewModel> ChangeRoleAsync(long userId, long roleId);
}