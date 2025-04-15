using Lotto.WebApi.ApiServices.Accounts;
using Lotto.WebApi.Models.Commons;
using Lotto.WebApi.Models.Users;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lotto.WebApi.Controllers;

public class AccountsController(IAccountApiService accountApiService) : BaseController
{
    [HttpPost("register")]
    public async ValueTask<IActionResult> RegisterAsync(UserRegisterModel registerModel)
    {
        await accountApiService.RegisterAsync(registerModel);
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",         
        });
    }

    [HttpGet("register-verify")]
    public async ValueTask<IActionResult> RegisterVerifyAsync(string email, string code)
    {
        await accountApiService.RegisterVerifyAsync(email, code);
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
        });
    }

    [HttpPost("login")]
    public async ValueTask<IActionResult> LoginAsync(string email, string password)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await accountApiService.LoginAsync(email, password)
        });
    }

    [HttpPost("send-code")]
    public async ValueTask<IActionResult> SendCodeAsync(string email)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await accountApiService.SendCodeAsync(email)
        });
    }

    [HttpPost("verify")]
    public async ValueTask<IActionResult> VerifyAsync(string email, string code)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await accountApiService.VerifyAsync(email, code)
        });
    }


    [HttpPost("reset-password")]
    public async ValueTask<IActionResult> ResetPasswordAsync(string email, string newPassword)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await accountApiService.ResetPasswordAsync(email, newPassword)
        });
    }
}