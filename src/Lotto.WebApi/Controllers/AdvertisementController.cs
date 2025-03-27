using Lotto.Domain.Entities.Advertisements;
using Lotto.Service.Configurations;
using Lotto.Service.Helpers;
using Lotto.WebApi.ApiServices.Advertisements;
using Lotto.WebApi.Models.Advertisements;
using Lotto.WebApi.Models.Commons;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Lotto.WebApi.Controllers
{
    public class AdvertisementController(IAdvertisementApiService advertisementApiService) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> Create(
        [FromForm] AdvertisementCreateModel model, 
        IFormFile file)
        {
            if (model == null || file == null)
                return BadRequest("Invalid input data.");

            var createdAd = await advertisementApiService.CreateAsync(model, file);
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Advertisement created successfully.",
                Data = createdAd 
            });
        }
        /*
        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            if (file == null)
                return BadRequest("File is required.");

            var fileUrl = await advertisementApiService.UploadFileAsync(file);
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "File uploaded successfully.",
                Data = fileUrl
            });
        }
        */

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

        [HttpDelete("{id}")]
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

        [HttpPost("log-view")]
        public async Task<IActionResult> LogView([FromQuery] long advertisementId)
        {
            if (User.Identity is not { IsAuthenticated: true }) 
                return Ok(new { message = "View not logged (unauthenticated user)" });

            await advertisementApiService.LogAdvertisementViewASync(GetUserId, advertisementId);
            return Ok(new Response
            {
                StatusCode = 200,
                Message = "Advertisement view logged successfully.",
                Data = null
            });
        }
    }
}
