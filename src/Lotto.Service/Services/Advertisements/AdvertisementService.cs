
using Lotto.Data.UnitOfWorks;
using Lotto.Domain.Entities.Advertisements;
using Lotto.Service.Configurations;
using Lotto.Service.Exceptions;
using Lotto.Service.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

namespace Lotto.Service.Services.Advertisements;

public class AdvertisementService(IUnitOfWork unitOfWork, IWebHostEnvironment _env) : IAdvertisementService
{
    // Umumiy fayl yuklash funksiyasi
    public async ValueTask<string> UploadFileAsync(IFormFile file, string fileType)
    {
        if (file == null || file.Length == 0)
            throw new ArgumentException("Invalid file.");

        // Ruxsat etilgan fayl formatlari
        var allowedExtensions = fileType.ToLower() switch
        {
            "video" => new[] { ".mp4", ".mov" },
            "image" => new[] { ".jpg", ".jpeg", ".png", ".gif" },
            _ => throw new ArgumentException("Unsupported file type.")
        };

        string fileExtension = Path.GetExtension(file.FileName).ToLower();

        if (!allowedExtensions.Contains(fileExtension))
            throw new ArgumentException($"Invalid file extension. Allowed: {string.Join(", ", allowedExtensions)}");

        // Maksimal hajm tekshiruvi (50MB video, 5MB rasm)
        long maxSize = fileType == "video" ? 50 * 1024 * 1024 : 5 * 1024 * 1024;
        if (file.Length > maxSize)
            throw new ArgumentException($"File is too large. Max size: {(maxSize / (1024 * 1024))}MB");

        // Faylni saqlash uchun papka
        string folderName = fileType == "video" ? "videos" : "images";
        string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", folderName);
    
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        // Fayl nomini yaratish
        string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);

        // Faylni saqlash
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        // **Frontend va backend ajratilgan bo'lsa, absolute URL qaytish kerak**
        string fileUrl = $"/uploads/{folderName}/{uniqueFileName}";
        return fileUrl;
    }


    public async ValueTask<Advertisement> CreateAsync(Advertisement advertisement, IFormFile? file = null, string? fileType = null)
    {
        // 📌 Agar fayl berilgan bo‘lsa, yuklab, URL'ni olish
        if (file != null && !string.IsNullOrWhiteSpace(fileType))
        {
            string fileUrl = await UploadFileAsync(file, fileType);
            advertisement.FileUrl = fileUrl;
        }

        await unitOfWork.AdvertisementRepository.InsertAsync(advertisement);
        await unitOfWork.SaveAsync();

        return advertisement;
    }

    public async ValueTask<IEnumerable<Advertisement>> GetAllAsync(PaginationParams @params, Filter filter)
    {
        var advertisements = unitOfWork.AdvertisementRepository
            .SelectAsQueryable().OrderBy(filter);

            // 📌 Paginatsiya
        var pagedAdvertisements = advertisements.ToPaginateAsQueryable(@params);
        return await pagedAdvertisements.ToListAsync();
    }

    public async ValueTask<Advertisement> GetByIdAsync(long id)
    {
        var advertisement = await unitOfWork.AdvertisementRepository
            .SelectAsQueryable(a => a.Id == id)
            .FirstOrDefaultAsync();

        if(advertisement == null)
            throw new NotFoundException($"Advertisement with ID {id} not found.");

        return advertisement;
    }

    public async ValueTask<Advertisement> UpdateAsync(long id, Advertisement advertisement)
    {
        var existingAd = await GetByIdAsync(id);

        existingAd.Title = advertisement.Title;
        existingAd.Content = advertisement.Content;
        existingAd.Url = advertisement.Url;
        existingAd.StartDate = advertisement.StartDate;
        existingAd.EndDate = advertisement.EndDate;
        existingAd.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.AdvertisementRepository.UpdateAsync(existingAd);
        await unitOfWork.SaveAsync();

        return existingAd;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var advertisement = await GetByIdAsync(id);

        await unitOfWork.AdvertisementRepository.DeleteAsync(advertisement);
        await unitOfWork.SaveAsync();

        return true;
    }
}
