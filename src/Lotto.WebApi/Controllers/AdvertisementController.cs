using Lotto.Domain.Entities.Advertisements;
using Lotto.Service.Configurations;
using Lotto.Service.Helpers;
using Lotto.WebApi.ApiServices.Advertisements;
using Lotto.WebApi.Models.Advertisements;
using Lotto.WebApi.Models.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Lotto.WebApi.Controllers
{
    public class AdvertisementController(IAdvertisementApiService advertisementApiService) : BaseController
    {
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Create(
        [FromForm] AdvertisementCreateModel model, 
        IFormFile file)
        {
            if (model == null)
                return BadRequest("Invalid input data.");

            var createdAd = await advertisementApiService.CreateAsync(model, file);
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Advertisement created successfully.",
                Data = createdAd 
            });
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var advertisement = await advertisementApiService.GetByIdAsync(id);
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Advertisement retrieved successfully.",
                Data = advertisement
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] PaginationParams @params, [FromQuery] Filter filter)
        {
            var advertisements = await advertisementApiService.GetAllAsync(@params, filter);
            return Ok(new Response 
            {
                StatusCode = 200,
                Message = "Advertisements retrieved successfully.",
                Data = advertisements
            });
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Update(long id, [FromBody] AdvertisementUpdateModel model)
        {
            var updatedAdv = await advertisementApiService.UpdateAsync(id, model);
            return Ok(new Response 
            {
                StatusCode = 200,
                Message = "Advertisements updated successfully.",
                Data = updatedAdv                
             });
        }

        [HttpPost("update-expired")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> UpdateExpiredAdvertisements()
        {
            try
            {
                await advertisementApiService.UpdateExpiredAdvertisementsAsync();
                return Ok(new Response
                {
                    StatusCode = 200,
                    Message = "Expired advertisements updated successfully.",
                    Data = null
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new Response
                {
                    StatusCode = 500,
                    Message = $"Error updating advertisements: {ex.Message}",
                    Data = null
                });
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Delete(long id)
        {
            var isDeleted = await advertisementApiService.DeleteAsync(id);
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Advertisement deleted successfully.",
                Data = null
            });
        }
    }
}
