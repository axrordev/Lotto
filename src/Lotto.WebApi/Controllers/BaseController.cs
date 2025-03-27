using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lotto.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public long GetUserId => Convert.ToInt64(HttpContext.User.FindFirst("Id")?.Value);
    }
}
