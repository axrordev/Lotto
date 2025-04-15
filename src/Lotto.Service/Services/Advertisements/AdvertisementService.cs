
using Lotto.Data.UnitOfWorks;
using Lotto.Domain.Entities.Advertisements;
using Lotto.Service.Configurations;
using Lotto.Service.Exceptions;
using Lotto.Service.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Lotto.Service.Helpers;
using Microsoft.VisualBasic.FileIO;

namespace Lotto.Service.Services.Advertisements;

public class AdvertisementService(IUnitOfWork unitOfWork) : IAdvertisementService
{
    // Umumiy fayl yuklash funksiyasi
    public async ValueTask<string> UploadFileAsync(IFormFile file)
    {
         var path = Path.Combine(FilePathHelper.WwwrootPath);
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

        // Fayl kengaytmasini olish
        string fileExtension = Path.GetExtension(file.FileName).ToLower();
        var imageExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
        var videoExtensions = new[] { ".mp4", ".mov" };

        string folderName;
        if (imageExtensions.Contains(fileExtension))
            folderName = "images";
        else if (videoExtensions.Contains(fileExtension))
            folderName = "videos";
        else
            throw new ArgumentException("Unsupported file type.");

        long maxSize = folderName == "videos" ? 50 * 1024 * 1024 : 5 * 1024 * 1024;
        if (file.Length > maxSize)
            throw new ArgumentException($"File is too large. Max size: {(maxSize / (1024 * 1024))}MB");

        // Faylni saqlash papkasini yaratish
        string uploadsFolder = Path.Combine(path, "uploads", folderName);
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        // Faylni saqlash
        string uniqueFileName = $"{Guid.NewGuid()}{fileExtension}";
        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
    
        using (var fileStream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(fileStream);
        }

        return $"/uploads/{folderName}/{uniqueFileName}";
    }



    public async ValueTask<Advertisement> CreateAsync(Advertisement advertisement, IFormFile? file = null)
    {
        // 📌 Fayl yuklash
        if (file != null)
        {
            try
            {
                string fileUrl = await UploadFileAsync(file);
                advertisement.FileUrl = fileUrl;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Fayl yuklashda xatolik yuz berdi.", ex);
            }
        }

        // 📌 Biznes logika
        advertisement.IsActive = true;
        if (advertisement.EndDate <= DateTime.UtcNow)
        {
            throw new ArgumentException("Tugash sanasi kelajakda bo‘lishi kerak.", nameof(advertisement.EndDate));
        }

        // 📌 Ma'lumotlarni saqlash
        try
        {
            var ad = await unitOfWork.AdvertisementRepository.InsertAsync(advertisement);
            await unitOfWork.SaveAsync();
            return ad;
        }
        catch (Exception ex)
        {
            // 📌 Xato haqida batafsil ma'lumot
            string errorMessage = ex.InnerException != null 
                ? $"Ma'lumotlarni saqlashda xatolik: {ex.InnerException.Message}" 
                : $"Ma'lumotlarni saqlashda xatolik: {ex.Message}";
            throw new InvalidOperationException(errorMessage, ex);
        }
    }

    public async ValueTask<IEnumerable<Advertisement>> GetAllAsync(PaginationParams @params, Filter filter)
    {
        var advertisements = unitOfWork.AdvertisementRepository
        .SelectAsQueryable()
        .Where(a => a.IsActive) // Faqat faol reklamalar
        .OrderBy(filter);

            // 📌 Paginatsiya
        var pagedAdvertisements = advertisements.ToPaginateAsQueryable(@params);
        return await pagedAdvertisements.ToListAsync();
    }

    public async ValueTask<Advertisement> GetByIdAsync(long id)
    {
        var advertisement = await unitOfWork.AdvertisementRepository
        .SelectAsQueryable(a => a.Id == id && a.IsActive) // Faqat faol reklama
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
        existingAd.EndDate = advertisement.EndDate;
        existingAd.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.AdvertisementRepository.UpdateAsync(existingAd);
        await unitOfWork.SaveAsync();

        return existingAd;
    }

    public async ValueTask UpdateExpiredAdvertisementsAsync()
    {
        var expiredAds = await unitOfWork.AdvertisementRepository
            .SelectAsQueryable(a => a.EndDate < DateTime.UtcNow && a.IsActive)
            .ToListAsync();

        foreach (var ad in expiredAds)
        {
            ad.IsActive = false;
            ad.UpdatedAt = DateTime.UtcNow;
            await unitOfWork.AdvertisementRepository.UpdateAsync(ad);
        }

        await unitOfWork.SaveAsync();
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var advertisement = await GetByIdAsync(id);

        await unitOfWork.AdvertisementRepository.DeleteAsync(advertisement);
        await unitOfWork.SaveAsync();

        return true;
    }

}
