
using Lotto.Domain.Entities.Games;
using Lotto.Service.Configurations;

namespace Lotto.Service.Services.Footballs;

public interface IFootballService
{
    ValueTask<Football> CreateAsync(Football football);
    ValueTask<PlayFootball> PlayAsync(PlayFootball playFootball);
    ValueTask<FootballResult> AddResultAsync(FootballResult footballResult);
    ValueTask AnnounceResultsAsync(long footballId);
    ValueTask<List<PlayFootball>> GetUserPlaysAsync(long userId);

    ValueTask<Football> GetByIdAsync(long id);
    ValueTask<List<Football>> GetAllAsync(PaginationParams @params, Filter filter);
    ValueTask<bool> DeleteAsync(long id);
}
