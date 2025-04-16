using AutoMapper;
using Lotto.Domain.Entities.Games;
using Lotto.Service.Configurations;
using Lotto.WebApi.Models.Numbers;
using Lotto.WebApi.Models.PlayNumbers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.Numbers
{
    public class NumberApiService(INumberService numberService, IMapper mapper) : INumberApiService
    {
        public async ValueTask<NumberViewModel> CreateAsync(NumberCreateModel createModel)
        {
            var number = mapper.Map<Number>(createModel);
            var createdNumber = await numberService.CreateAsync(number);
            return mapper.Map<NumberViewModel>(createdNumber);
        }

        public async ValueTask<PlayNumberViewModel> PlayAsync(PlayNumberCreateModel createModel)
        {
            var playNumber = mapper.Map<PlayNumber>(createModel);
            var createdPlayNumber = await numberService.PlayAsync(playNumber);
            return mapper.Map<PlayNumberViewModel>(createdPlayNumber);
        }

        public async ValueTask<IEnumerable<PlayNumberViewModel>> GetUserPlaysAsync(long userId)
        {
            var plays = await numberService.GetUserPlaysAsync(userId);
            return mapper.Map<IEnumerable<PlayNumberViewModel>>(plays);
        }

        public async ValueTask SetWinningNumbersAsync(SetWinningNumbersModel model)
        {
            await numberService.SetWinningNumbersAsync(model.NumberId, model.WinningNumbers);
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
