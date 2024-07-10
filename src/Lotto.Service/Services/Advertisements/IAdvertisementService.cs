using Lotto.Domain.Entities.Advertisements;

namespace Lotto.Service.Services.Advertisements;

public interface IAdvertisementService
{
    Task<Advertisement> CreateAsync(Advertisement advertisement);
    Task<Advertisement> UpdateAsync(long id, Advertisement advertisement);
    Task<bool> DeleteAsync(long id);
    Task<Advertisement> GetByIdAsync(long id);
    Task<IEnumerable<Advertisement>> GetAllAsync(PaginationParams @params);
}
