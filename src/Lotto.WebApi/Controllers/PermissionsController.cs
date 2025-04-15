
using Lotto.Service.Configurations;
using Lotto.WebApi.ApiServices.Permissions;
using Lotto.WebApi.Models.Commons;
using Lotto.WebApi.Models.Permissions;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lotto.WebApi.Controllers;

public class PermissionsController(IPermissionApiService permissionApiService) : BaseController
{
    [HttpPost]
    public async ValueTask<IActionResult> PostAsync(PermissionCreateModel createModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await permissionApiService.CreateAsync(createModel)
        });
    }

    [HttpPut("{id:long}")]
    public async ValueTask<IActionResult> PutAsync(long id, PermissionUpdateModel updateModel)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await permissionApiService.UpdateAsync(id, updateModel)
        });
    }

    [HttpDelete("{id:long}")]
    public async ValueTask<IActionResult> DeleteAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await permissionApiService.DeleteAsync(id)
        });
    }

    [HttpGet("{id:long}")]
    public async ValueTask<IActionResult> GetAsync(long id)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await permissionApiService.GetByIdAsync(id)
        });
    }

    [HttpGet]
    public async ValueTask<IActionResult> GetListAsync(
        [FromQuery] PaginationParams @params,
        [FromQuery] Filter filter,
        [FromQuery] string search = null)
    {
        return Ok(new Response
        {
            StatusCode = 200,
            Message = "Success",
            Data = await permissionApiService.GetAllAsync(@params, filter)
        });
    }
}
