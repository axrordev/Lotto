using Lotto.Service.Configurations;
using Lotto.WebApi.Models.Numbers;
using Lotto.WebApi.Models.PlayNumbers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.Numbers
{
    public interface INumberApiService
    {
        ValueTask<NumberViewModel> CreateAsync(NumberCreateModel createModel);
        ValueTask<PlayNumberViewModel> PlayAsync(PlayNumberCreateModel createModel);
        ValueTask AnnounceResultsAsync(long numberId);
        ValueTask<IEnumerable<PlayNumberViewModel>> GetUserPlaysAsync(long userId);

        ValueTask<NumberViewModel> UpdateAsync(long id, NumberUpdateModel updateModel);
        ValueTask<bool> DeleteAsync(long id);
        ValueTask<NumberViewModel> GetByIdAsync(long id);
        ValueTask<IEnumerable<NumberViewModel>> GetAllAsync();
        ValueTask<IEnumerable<NumberViewModel>> GetAllAsync(PaginationParams @params, Filter filter);
    }
}
