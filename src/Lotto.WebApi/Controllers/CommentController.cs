using Lotto.Service.Configurations;
using Lotto.WebApi.ApiServices.Comments;
using Lotto.WebApi.Models.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Lotto.WebApi.Models.Comments;

namespace Lotto.WebApi.Controllers
{
public class CommentController( ICommentApiService _commentApiService) : BaseController
    {
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateAsync([FromBody] CommentCreateModel createModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new Response
                    {
                        StatusCode = 400,
                        Message = "Invalid input data.",
                        Data = ModelState
                    });
                }

                var createdComment = await _commentApiService.CreateAsync(GetUserId, createModel);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Comment created successfully!",
                    Data = createdComment
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error creating comment: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPut("{commentId}")]
        [Authorize]
        public async Task<IActionResult> UpdateAsync(long commentId, [FromBody] CommentUpdateModel updateModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new Response
                    {
                        StatusCode = 400,
                        Message = "Invalid input data.",
                        Data = ModelState
                    });
                }
              
                var updatedComment = await _commentApiService.UpdateAsync(GetUserId, commentId, updateModel);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Comment updated successfully!",
                    Data = updatedComment
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error updating comment: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpDelete("{commentId}")]
        [Authorize]
        public async Task<IActionResult> DeleteAsync(long commentId)
        {
            try
            {
                var currentUserId = long.Parse(User.FindFirst("Id")?.Value ?? throw new UnauthorizedAccessException("User ID not found in token."));
                var result = await _commentApiService.DeleteAsync(currentUserId, commentId);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Comment deleted successfully!",
                    Data = result
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error deleting comment: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("{id}")]
        [Authorize]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            try
            {
                var comment = await _commentApiService.GetByIdAsync(id);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Comment retrieved successfully!",
                    Data = comment
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error retrieving comment: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, [FromQuery] Filter filter)
        {
            try
            {
                var comments = await _commentApiService.GetAllAsync(@params, filter);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Comments retrieved successfully!",
                    Data = comments
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error retrieving comments: {ex.Message}",
                    Data = null
                });
            }
        }
    }
}
