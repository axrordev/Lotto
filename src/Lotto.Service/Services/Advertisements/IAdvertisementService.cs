using Lotto.Domain.Entities.Advertisements;
using Lotto.Service.Configurations;
using Microsoft.AspNetCore.Http;

namespace Lotto.Service.Services.Advertisements;

public interface IAdvertisementService
{
    ValueTask<string> UploadFileAsync(IFormFile file);
    ValueTask<Advertisement> CreateAsync(Advertisement advertisement, IFormFile file);
    ValueTask<Advertisement> UpdateAsync(long id, Advertisement advertisement);
    ValueTask UpdateExpiredAdvertisementsAsync();
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Advertisement> GetByIdAsync(long id);
    ValueTask<IEnumerable<Advertisement>> GetAllAsync(PaginationParams @params, Filter filter);
}
