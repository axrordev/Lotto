using Lotto.Domain.Entities.Advertisements;
using Lotto.Service.Configurations;
using Microsoft.AspNetCore.Http;

namespace Lotto.Service.Services.Advertisements;

public interface IAdvertisementService
{
    ValueTask<string> UploadFileAsync(IFormFile file, string fileType);
    ValueTask<Advertisement> CreateAsync(Advertisement advertisement, IFormFile file, string fileType);
    ValueTask<Advertisement> UpdateAsync(long id, Advertisement advertisement);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Advertisement> GetByIdAsync(long id);
    ValueTask<IEnumerable<Advertisement>> GetAllAsync(PaginationParams @params, Filter filter);
}
