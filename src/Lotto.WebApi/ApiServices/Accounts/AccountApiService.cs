using AutoMapper;
using Lotto.Domain.Entities.Users;
using Lotto.Service.Services.Accounts;
using Lotto.Service.Services.UserRolePermissions;
using Lotto.WebApi.Extensions;
using Lotto.WebApi.Models.Permissions;
using Lotto.WebApi.Models.Users;
using Lotto.WebApi.Validators.Accounts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.Accounts;

public class AccountApiService(
    IMapper mapper,
    IAccountService accountService,
    AccountLoginValidator loginValidator,
    AccountVerifyValidator verifyValidator,
    AccountCreateValidator createValidator,
    AccountSendCodeValidator sendCodeValidator,
    AccountRegisterModelValidator registerValidator,
    AccountResetPasswordValidator resetPasswordValidator,
    IUserRolePermissionService userRolePermissionService) : IAccountApiService
{
    public async ValueTask RegisterAsync(UserRegisterModel registerModel)
    {
        await registerValidator.EnsureValidatedAsync(registerModel);
        await accountService.RegisterAsync(mapper.Map<User>(registerModel));
    }

    public async ValueTask RegisterVerifyAsync(string email, string code)
    {
        await verifyValidator.EnsureValidatedAsync(email, code);
        await accountService.RegisterVerifyAsync(email, code);

        await createValidator.EnsureValidatedAsync(email);
        await accountService.CreateAsync(email);
    }

    public async ValueTask<LoginViewModel> LoginAsync(string email, string password)
    {
        await loginValidator.EnsureValidatedAsync(email, password);
        var result = await accountService.LoginAsync(email, password);
        var rolePermissions = await userRolePermissionService.GetAlByRoleIdAsync(result.user.RoleId);
        var mappedResult = mapper.Map<LoginViewModel>(result.user);
        var permissions = rolePermissions.Select(p => p.Permission);
        mappedResult.Permissions = mapper.Map<IEnumerable<PermissionViewModel>>(permissions);
        mappedResult.Token = result.token;
        return mappedResult;
    }

    public async ValueTask<bool> SendCodeAsync(string email)
    {
        await sendCodeValidator.EnsureValidatedAsync(email);
        return await accountService.SendCodeAsync(email);
    }

    public async ValueTask<bool> VerifyAsync(string email, string code)
    {
        await verifyValidator.EnsureValidatedAsync(email, code);
        return await accountService.VerifyAsync(email, code);
    }

    public async ValueTask<UserViewModel> ResetPasswordAsync(string email, string newPassword)
    {
        await resetPasswordValidator.EnsureValidatedAsync(email, newPassword);
        var result = await accountService.ResetPasswordAsync(email, newPassword);
        return mapper.Map<UserViewModel>(result);
    }
}
