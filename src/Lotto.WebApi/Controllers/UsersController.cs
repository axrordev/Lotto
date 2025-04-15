
using Lotto.Service.Configurations;
using Lotto.WebApi.ApiServices.Users;
using Lotto.WebApi.Models.Commons;
using Lotto.WebApi.Models.Users;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Lotto.WebApi.Controllers;

public class UsersController(IUserApiService userApiService) : BaseController
{
    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(UserUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userApiService.ModifyAsync(GetUserId, updateModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync()
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userApiService.DeleteAsync(GetUserId)
        });
    }

    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync()
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userApiService.GetAsync(GetUserId)
        });
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetListAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userApiService.GetAllAsync(@params, filter)
        });
    }

    [HttpPatch("change-password")]
    public async ValueTask<IActionResult> ChangePasswordAsync(
        string oldPassword,
        string newPassword,
        string confirmPassword)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userApiService.ChangePasswordAsync(oldPassword, newPassword, confirmPassword)
        });
    }

    [HttpPatch("change-role")]
    public async ValueTask<IActionResult> ChangeRoleAsync(long userId, long roleId)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userApiService.ChangeRoleAsync(userId, roleId)
        });
    }
}