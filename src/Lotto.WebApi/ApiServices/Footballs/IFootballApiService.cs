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
}
