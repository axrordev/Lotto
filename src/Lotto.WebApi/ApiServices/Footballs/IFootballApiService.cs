using Lotto.Domain.Entities.Games;
using Lotto.Service.Configurations;
using Lotto.WebApi.Models.Footballs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.Footballs;

public interface IFootballApiService
{
    ValueTask<FootballViewModel> CreateAsync(FootballCreateModel createModel);
    ValueTask<PlayFootballViewModel> PlayAsync(PlayFootballCreateModel createModel);
    ValueTask<FootballResultViewModel> AddResultAsync(FootballResultCreateModel createModel);
    ValueTask AnnounceResultsAsync(long footballId);
    ValueTask<IEnumerable<PlayFootballViewModel>> GetUserPlaysAsync(long userId);

    ValueTask<FootballViewModel> GetByIdAsync(long id);
    ValueTask<List<FootballViewModel>> GetAllAsync(PaginationParams @params, Filter filter);
    ValueTask<bool> DeleteAsync(long id);
}
