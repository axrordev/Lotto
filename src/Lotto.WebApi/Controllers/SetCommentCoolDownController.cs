using Lotto.Service.Services.CommentSettings;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lotto.WebApi.Controllers
{
    public class SetCommentCoolDownController(ICommentSettingService commentSettingService) : BaseController
    {
        [Authorize(Roles = "admin")] // 📌 Faqat adminlar o'zgartira olishi kerak
        [HttpPut("set-comment-cooldown")]
        public async Task<IActionResult> SetCommentCooldown([FromBody] int cooldown)
        {
            await commentSettingService.SetCommentCooldownAsync(cooldown);
    
            return Ok(new 
            { 
                Message = "Comment cooldown updated", 
                Cooldown = cooldown 
            });
        }

    }
}
