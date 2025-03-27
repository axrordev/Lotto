using Lotto.WebApi.Helpers;
using Lotto.WebApi.Models.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Security.Claims;

namespace Lotto.WebApi.ServiceAuths;

public class CustomAuthorize : AuthorizeAttribute, IAuthorizationFilter
{
    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var actionName = context.ActionDescriptor.RouteValues["action"].ToString();
        var controllerName = context.ActionDescriptor.RouteValues["controller"].ToString();
        var role = context.HttpContext.User.FindFirst(ClaimTypes.Role).Value;

        var user = context.HttpContext.User;
        if (!user.Identity.IsAuthenticated)
        {
            context.Result = new UnauthorizedResult();
            return;
        }

        var userRoleService = ServiceHelper.UserRoleService;
        var userRole = userRoleService
            .GetByNameAsync(role)
            .GetAwaiter()
            .GetResult();

        var userRolePermissionService = ServiceHelper.UserRolePermissionService;
        var permissions = userRolePermissionService
            .GetAlByRoleIdAsync(userRole.Id)
            .GetAwaiter()
            .GetResult();

        var givenPermissions = permissions
            .Where(p => p.Permission.Controller == controllerName && p.Permission.Action == actionName).ToList();

        if (!givenPermissions.Any())
        {
            context.HttpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

            context.Result = new ObjectResult(new Response
            {
                StatusCode = StatusCodes.Status403Forbidden,
                Message = "You don't have permission for this endpoint!",
            });
        }
    }
}
