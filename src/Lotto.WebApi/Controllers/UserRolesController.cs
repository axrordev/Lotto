
using Lotto.Service.Configurations;
using Lotto.WebApi.ApiServices.UserRoles;
using Lotto.WebApi.Models.Commons;
using Lotto.WebApi.Models.UserRoles;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;


namespace Lotto.WebApi.Controllers;

public class UserRolesController(IUserRoleApiService userRoleApiService) : BaseController
{
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync(UserRoleCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userRoleApiService.CreateAsync(createModel)
        });
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, UserRoleUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userRoleApiService.UpdateAsync(id, updateModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userRoleApiService.DeleteAsync(id)
        });
    }

    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await userRoleApiService.GetByIdAsync(id)
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
            Data = await userRoleApiService.GetAllAsync(@params, filter)
        });
    }
}
