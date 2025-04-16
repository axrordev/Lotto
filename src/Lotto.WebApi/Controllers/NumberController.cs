using Lotto.WebApi.ApiServices.Numbers;
using Lotto.WebApi.Models.Commons;
using Lotto.WebApi.Models.Numbers;
using Lotto.WebApi.Models.PlayNumbers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Lotto.WebApi.Controllers
{
    public class NumberController(INumberApiService numberApiService) : BaseController
    {
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> CreateAsync([FromBody] NumberCreateModel model)
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

                var createdNumber = await numberApiService.CreateAsync(model);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Number game created successfully!",
                    Data = createdNumber
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error creating number game: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPost("play")]
        [Authorize]
        public async Task<IActionResult> PlayAsync([FromBody] PlayNumberCreateModel model)
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

                var createdPlayNumber = await numberApiService.PlayAsync(model);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Successfully participated in the number game!",
                    Data = createdPlayNumber
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error playing number game: {ex.Message}",
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
                var plays = await numberApiService.GetUserPlaysAsync(userId);
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

        [HttpPost("set-winning-numbers")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> SetWinningNumbersAsync([FromBody] SetWinningNumbersModel model)
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

                await numberApiService.SetWinningNumbersAsync(model);
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Winning numbers set successfully!",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error setting winning numbers: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(long id, [FromBody] NumberUpdateModel model)
        {
            var updatedNumber = await numberApiService.UpdateAsync(id, model);
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Number muvaffaqiyatli yangilandi!",
                Data = updatedNumber
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(long id)
        {
            var isDeleted = await numberApiService.DeleteAsync(id);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Number muvaffaqiyatli o‘chirildi!",
                Data = null
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(long id)
        {
            var number = await numberApiService.GetByIdAsync(id);

            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Number topildi!",
                Data = number
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var numbers = await numberApiService.GetAllAsync();
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Hamma numberlar olindi!",
                Data = numbers
            });
        }  
    }
}
