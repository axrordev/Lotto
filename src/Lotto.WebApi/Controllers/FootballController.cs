using Lotto.WebApi.ApiServices.Footballs;
using Lotto.WebApi.Models.Commons;
using Lotto.WebApi.Models.Footballs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using Lotto.Service.Configurations;
using Lotto.Service.Exceptions;

namespace Lotto.WebApi.Controllers
{
    public class FootballController(IFootballApiService footballApiService) : BaseController
    {
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateAsync([FromBody] FootballCreateModel model)
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

                var createdFootball = await footballApiService.CreateAsync(model);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Football game muvaffaqiyatli yaratildi!",
                    Data = createdFootball
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error creating football game: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPost("play")]
        [Authorize]
        public async Task<IActionResult> PlayAsync([FromBody] PlayFootballCreateModel model)
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

                var createdPlayFootball = await footballApiService.PlayAsync(model);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Successfully participated in the football game!",
                    Data = createdPlayFootball
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error playing football game: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPost("result")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AddResultAsync([FromBody] FootballResultCreateModel model)
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

                var createdFootballResult = await footballApiService.AddResultAsync(model);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Football result added successfully!",
                    Data = createdFootballResult
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error adding football result: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPost("{id}/announce-results")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> AnnounceResultsAsync(long id)
        {
            try
            {
                await footballApiService.AnnounceResultsAsync(id);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Results announced successfully!",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error announcing results: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("my-plays/{userId}")]
        [Authorize]
        public async Task<IActionResult> GetUserPlaysAsync(long userId)
        {
            try
            {
                var plays = await footballApiService.GetUserPlaysAsync(userId);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "User plays retrieved successfully!",
                    Data = plays
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error retrieving user plays: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            try
            {
                var football = await footballApiService.GetByIdAsync(id);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Football game retrieved successfully!",
                    Data = football
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new Response
                {
                    StatusCode = 404,
                    Message = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error retrieving football game: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync([FromQuery] PaginationParams @params, [FromQuery] Filter filter)
        {
            try
            {
                var footballs = await footballApiService.GetAllAsync(@params, filter);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Football games retrieved successfully!",
                    Data = footballs
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error retrieving football games: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")] // Faqat admin o'chira oladi
        public async Task<IActionResult> DeleteAsync(long id)
        {
            try
            {
                var result = await footballApiService.DeleteAsync(id);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Football game deleted successfully!",
                    Data = result
                });
            }
            catch (NotFoundException ex)
            {
                return NotFound(new Response
                {
                    StatusCode = 404,
                    Message = ex.Message,
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error deleting football game: {ex.Message}",
                    Data = null
                });
            }
        }
        }
}
