using AutoMapper;
using Lotto.Domain.Entities.Users;
using Lotto.Service.Configurations;
using Lotto.Service.Services.Users;
using Lotto.WebApi.Models.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.Users;

public class UserApiService(IUserService userService, IMapper mapper) : IUserApiService
{
    public async ValueTask<UserViewModel> ModifyAsync(long id, UserUpdateModel updateModel)
    {
        var mappedUser = mapper.Map<User>(updateModel);
        var updatedUser = await userService.ModifyAsync(id, mappedUser);
        return mapper.Map<UserViewModel>(updatedUser);
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        return await userService.DeleteAsync(id);
    }

    public async ValueTask<UserViewModel> GetAsync(long id)
    {
        return mapper.Map<UserViewModel>(await userService.GetAsync(id));
    }

    public async ValueTask<IEnumerable<UserViewModel>> GetAllAsync(PaginationParams @params, Filter filter)
    {
        var result = await userService.GetAllAsync(@params, filter);
        return mapper.Map<IEnumerable<UserViewModel>>(result);
    }

    public async ValueTask<UserViewModel> ChangePasswordAsync(string oldPasword, string newPassword, string confirmPassword)
    {
        var result = await userService.ChangePasswordAsync(oldPasword, newPassword, confirmPassword);
        return mapper.Map<UserViewModel>(result);
    }

    public async ValueTask<UserViewModel> ChangeRoleAsync(long userId, long roleId)
    {
        var result = await userService.ChangeRoleAsync(userId, roleId);
        return mapper.Map<UserViewModel>(result);
    }
}
