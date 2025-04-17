using Lotto.WebApi.ApiServices.CommentServices;
using Lotto.WebApi.Models.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Lotto.WebApi.Models.Comments;

namespace Lotto.WebApi.Controllers
{
    public class CommentSettingController(ICommentSettingApiService _commentSettingApiService) : BaseController
    {
        [HttpGet("cooldown")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> GetCommentCooldownAsync()
        {
            try
            {
                var cooldown = await _commentSettingApiService.GetCommentCooldownAsync();
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Comment cooldown retrieved successfully!",
                    Data = cooldown
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error retrieving comment cooldown: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPost("cooldown")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> SetCommentCooldownAsync([FromBody] CommentCooldownModel model)
        {
            try
            {
                if (!ModelState.IsValid || model.Cooldown < 0)
                {
                    return BadRequest(new Response
                    {
                        StatusCode = 400,
                        Message = "Invalid input data. Cooldown cannot be negative.",
                        Data = ModelState
                    });
                }

                await _commentSettingApiService.SetCommentCooldownAsync(model.Cooldown);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Comment cooldown set successfully!",
                    Data = model.Cooldown
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error setting comment cooldown: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}
