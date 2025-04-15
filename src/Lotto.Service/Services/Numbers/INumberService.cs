using Lotto.Domain.Entities.Games;
using Lotto.Service.Configurations;
using System;
using System.Threading.Tasks;

public interface INumberService
{
    ValueTask<Number> CreateAsync(Number number, int[] winningNumbers);
    ValueTask<PlayNumber> PlayAsync(PlayNumber playNumber);
    ValueTask AnnounceResultsAsync(long numberId);
    ValueTask<List<PlayNumber>> GetUserPlaysAsync(long userId);

    ValueTask<Number> UpdateAsync(long id, Number number);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Number> GetByIdAsync(long id);
    ValueTask<IEnumerable<Number>> GetAllAsync();
    ValueTask<IEnumerable<Number>> GetAllAsync(PaginationParams @params, Filter filter);
}