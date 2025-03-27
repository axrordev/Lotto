using Lotto.WebApi.ApiServices.Numbers;
using Lotto.WebApi.Models.Commons;
using Lotto.WebApi.Models.Numbers;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Lotto.WebApi.Controllers
{
    public class NumberController(INumberApiService numberApiService) : BaseController
    {
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] NumberCreateModel model)
        {
            var createdNumber = await numberApiService.CreateAsync(model);
            return Ok(new Response
            {
                StatusCode = 201,
                Message = "Number muvaffaqiyatli yaratildi!",
                Data = createdNumber
            });
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

        [HttpGet("{gameId}/download")]
        public async Task<IActionResult> DownloadWinningNumbers(long gameId)
        {
            string folderPath = Path.Combine("wwwroot", "archives");
            string zipPath = Path.Combine(folderPath, gameId + ".zip.enc");

            if (!System.IO.File.Exists(zipPath))
                return NotFound(new Response
                {
                    StatusCode = 404,
                    Message = "ZIP fayl topilmadi!",
                    Data = null
                });

            var memory = new MemoryStream();
            using (var stream = new FileStream(zipPath, FileMode.Open, FileAccess.Read))
            {
                await stream.CopyToAsync(memory);
            }
            memory.Position = 0;

            return File(memory, "application/octet-stream", $"{gameId}.zip.enc");
        }
    }
}
