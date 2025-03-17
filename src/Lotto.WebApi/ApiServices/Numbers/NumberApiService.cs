using AutoMapper;
using Lotto.Domain.Entities.Games;
using Lotto.Service.Configurations;
using Lotto.WebApi.Models.Numbers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.Numbers
{
    public class NumberApiService(INumberService numberService, IMapper mapper) : INumberApiService
    {
        public async ValueTask<NumberViewModel> CreateAsync(NumberCreateModel createModel)
        {
            var createdNumber = await numberService.CreateAsync(mapper.Map<Number>(createModel));
            return  mapper.Map<NumberViewModel>(createdNumber);
        }

        public async ValueTask<bool> DeleteAsync(long id)
        {
            return await numberService.DeleteAsync(id);
        }

        public async ValueTask<IEnumerable<NumberViewModel>> GetAllAsync(PaginationParams @params, Filter filter)
        {
            var numbers = await numberService.GetAllAsync(@params, filter);
            return mapper.Map<IEnumerable<NumberViewModel>>(numbers);
        }

        public async ValueTask<IEnumerable<NumberViewModel>> GetAllAsync()
        {
            var numbers = await numberService.GetAllAsync();
            return mapper.Map<IEnumerable<NumberViewModel>>(numbers);
        }

        public async ValueTask<NumberViewModel> GetByIdAsync(long id)
        {
            var number = await numberService.GetByIdAsync(id);
            return mapper.Map<NumberViewModel>(number);
        }

        public async ValueTask<NumberViewModel> UpdateAsync(long id, NumberUpdateModel updateModel)
        {
            var updatedNumber = await numberService.UpdateAsync(id, mapper.Map<Number>(updateModel));
            return mapper.Map<NumberViewModel>(updatedNumber);
        }
    }
}
