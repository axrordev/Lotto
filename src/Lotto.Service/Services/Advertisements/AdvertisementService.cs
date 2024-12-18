using Lotto.Data.Repositories;
using Lotto.Domain.Entities.Advertisements;
using Lotto.Service.Configurations;
using Lotto.Service.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.Numerics;

namespace Lotto.Service.Services.Advertisements;

public class AdvertisementService(IWebHostEnvironment _env) : IAdvertisementService
{
    // Umumiy fayl yuklash funksiyasi
    public async Task<string> UploadFileAsync(IFormFile file, string fileType)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid file.");

        // Ruxsat etilgan fayl formatlarini belgilash
        var allowedExtensions = fileType.ToLower() switch
        {
            "video" => new[] { ".mp4", ".mov" },
            "image" => new[] { ".jpg", ".jpeg", ".png", ".gif" },
            _ => throw new ArgumentException("Unsupported file type.")
        };

        string fileExtension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(fileExtension))
            throw new ArgumentException($"Invalid file extension. Allowed extensions are: {string.Join(", ", allowedExtensions)}");

        // Faylni saqlash uchun papka yo'lini belgilash
        string folderName = fileType == "video" ? "videos" : "images";
        string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", folderName);
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        // Faylni saqlash uchun unikal nom yaratish
        string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        // Faylni saqlash
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        // URL qaytarish
        string fileUrl = $"/uploads/{folderName}/{uniqueFileName}";
        return fileUrl;
    }

    public Task<Advertisement> CreateAsync(Advertisement advertisement)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Advertisement>> GetAllAsync(PaginationParams @params, Filter filter)
    {
        throw new NotImplementedException();
    }

    public Task<Advertisement> GetByIdAsync(long id)
    {
        throw new NotImplementedException();
    }

    public Task<Advertisement> UpdateAsync(long id, Advertisement advertisement)
    {
        throw new NotImplementedException();
    }

    public Task<bool> DeleteAsync(long id)
    {
        throw new NotImplementedException();
    }
}
