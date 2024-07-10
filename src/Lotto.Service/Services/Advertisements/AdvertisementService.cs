using Lotto.Data.Repositories;
using Lotto.Domain.Entities.Advertisements;
using Planner.Service.Exceptions;
using System.Numerics;

namespace Lotto.Service.Services.Advertisements;

public class AdvertisementService(IRepository<Advertisement> repository) : IAdvertisementService
{
    public async Task<Advertisement> CreateAsync(Advertisement advertisement)
    {
        var createdAdvertisement = await repository.InsertAsync(advertisement);
        await repository.SaveAsync();

        return createdAdvertisement;
    }

    public async Task<Advertisement> UpdateAsync(long id, Advertisement advertisement)
    {
        var existAdvertisement = await repository.SelectAsync(ad => ad.Id == id)
            ?? throw new AlreadyExistException("Advertisement is not found");

        existAdvertisement.EndDate = advertisement.EndDate;
        existAdvertisement.Description = advertisement.Description;
        existAdvertisement.UpdatedAt = DateTime.UtcNow;

        var updatedAdvertisement = await repository.UpdateAsync(existAdvertisement);
        await repository.SaveAsync();

        return updatedAdvertisement;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var existAdvertisement = await repository.SelectAsync(ad => ad.Id == id)
           ?? throw new AlreadyExistException("Advertisement is not found");

        
        await repository.SaveAsync();

        return true;
    }

    public async Task<IEnumerable<Advertisement>> GetAllAsync(PaginationParams @params)
    {
        throw new NotImplementedException();
    }

    public async Task<Advertisement> GetByIdAsync(long id)
    {
        var existAdvertisement = 
    }
}
