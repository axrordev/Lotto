using AutoMapper;
using Lotto.Domain.Entities.Advertisements;
using Lotto.Service.Configurations;
using Lotto.Service.Services.Advertisements;
using Lotto.WebApi.Models.Advertisements;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.Advertisements
{
    public class AdvertisementApiService(IAdvertisementService advertisementService, IMapper mapper) : IAdvertisementApiService
    {
        //public async ValueTask<string> UploadFileAsync(IFormFile file)
        //{
        //    var uploadAd = await advertisementService.UploadFileAsync(file);
        //    return uploadAd;
        //}

        public async ValueTask<AdvertisementViewModel> CreateAsync(AdvertisementCreateModel createModel, IFormFile file)
        {
            var createdAd = await advertisementService.CreateAsync(mapper.Map<Advertisement>(createModel), file);
            return mapper.Map<AdvertisementViewModel>(createdAd);
        }

        public async ValueTask LogAdvertisementViewASync(long userId, long adId)
        {
            await advertisementService.LogAdvertisementViewASync(userId, adId);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            return await advertisementService.DeleteAsync(id);
        }

        public async ValueTask<IEnumerable<AdvertisementViewModel>> GetAllAsync(PaginationParams @params, Filter filter)
        {
            var result = await advertisementService.GetAllAsync(@params, filter);
            return mapper.Map<IEnumerable<AdvertisementViewModel>>(result);
        }

        public async ValueTask<AdvertisementViewModel> GetByIdAsync(long id)
        {
            var result = await advertisementService.GetByIdAsync(id);
            return mapper.Map<AdvertisementViewModel>(result);
        }

        public async ValueTask<AdvertisementViewModel> UpdateAsync(long id, AdvertisementUpdateModel updateModel)
        {
            var updatedAd = await advertisementService.UpdateAsync(id, mapper.Map<Advertisement>(updateModel));
            return mapper.Map<AdvertisementViewModel>(updatedAd);
        }
    }
}
