using Lotto.WebApi.Models.Users;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.Accounts;

public interface IAccountApiService
{
    ValueTask RegisterAsync(UserRegisterModel registerModel);
    ValueTask RegisterVerifyAsync(string email, string code);
    ValueTask<LoginViewModel> LoginAsync(string email, string password);
    ValueTask<bool> SendCodeAsync(string email);
    ValueTask<bool> VerifyAsync(string email, string code);
    ValueTask<UserViewModel> ResetPasswordAsync(string email, string newPassword);
}