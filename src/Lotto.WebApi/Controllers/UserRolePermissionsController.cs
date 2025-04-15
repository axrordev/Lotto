
using Lotto.Service.Configurations;
using Lotto.WebApi.ApiServices.UserRolePermissions;
using Lotto.WebApi.Models.Commons;
using Lotto.WebApi.Models.UserRolePermissions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lotto.WebApi.Controllers;

public class UserRolePermissionsController(IUserRolePermissionApiService userRolePermissionApiService) : BaseController
{
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync(UserRolePermissionCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userRolePermissionApiService.CreateAsync(createModel)
        });
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, UserRolePermissionUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userRolePermissionApiService.UpdateAsync(id, updateModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userRolePermissionApiService.DeleteAsync(id)
        });
    }

    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userRolePermissionApiService.GetByIdAsync(id)
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
            Data = await userRolePermissionApiService.GetAllAsync(@params, filter)
        });
    }
}
