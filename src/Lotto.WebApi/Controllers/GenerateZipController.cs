using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System;
using Lotto.Service.Helpers;

namespace Lotto.WebApi.Controllers
{
    public class GenerateZipController : BaseController
    {
        /*
        [HttpPost("generate-zip/{gameId}")]
        public async Task<IActionResult> GenerateZip(long gameId)
        {
            var game = await _context.Numbers.FindAsync(gameId);
            if (game == null) return NotFound("O'yin topilmadi!");

            string password = Guid.NewGuid().ToString("N").Substring(0, 12); // Tasodifiy 12-belgili parol
            string zipPath = $"wwwroot/archives/{gameId}.zip";

            ZipService.CreateEncryptedZip(zipPath, password, string.Join(", ", game.WinningNumbers));

            game.SecretPassword = password;
            await _context.SaveChangesAsync();

            return Ok(new { DownloadUrl = $"/archives/{gameId}.zip" });
        }
        */
    }
}
