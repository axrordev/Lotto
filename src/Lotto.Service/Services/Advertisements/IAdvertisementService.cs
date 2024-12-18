using Lotto.Domain.Entities.Advertisements;
using Lotto.Service.Configurations;
using Microsoft.AspNetCore.Http;

namespace Lotto.Service.Services.Advertisements;

public interface IAdvertisementService
{
    Task<string> UploadFileAsync(IFormFile file, string fileType);
    Task<Advertisement> CreateAsync(Advertisement advertisement);
    Task<Advertisement> UpdateAsync(long id, Advertisement advertisement);
    Task<bool> DeleteAsync(long id);
    Task<Advertisement> GetByIdAsync(long id);
    Task<IEnumerable<Advertisement>> GetAllAsync(PaginationParams @params, Filter filter);
}
