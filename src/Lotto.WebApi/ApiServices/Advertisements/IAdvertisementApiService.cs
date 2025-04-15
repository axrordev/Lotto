using Lotto.Domain.Entities.Advertisements;
using Lotto.Service.Configurations;
using Lotto.WebApi.Models.Advertisements;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.Advertisements;

public interface IAdvertisementApiService
{
    //ValueTask<string> UploadFileAsync(IFormFile file);
    ValueTask<AdvertisementViewModel> CreateAsync(AdvertisementCreateModel createModel, IFormFile file);
    ValueTask<AdvertisementViewModel> UpdateAsync(long id, AdvertisementUpdateModel updateModel);
    ValueTask UpdateExpiredAdvertisementsAsync();
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<AdvertisementViewModel> GetByIdAsync(long id);
    ValueTask<IEnumerable<AdvertisementViewModel>> GetAllAsync(PaginationParams @params, Filter filter);
}
