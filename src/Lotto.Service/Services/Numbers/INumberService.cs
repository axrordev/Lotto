using Lotto.Domain.Entities.Games;
using Lotto.Service.Configurations;
using System;
using System.Threading.Tasks;

public interface INumberService
{
    ValueTask<Number> CreateAsync(Number number);
    ValueTask<PlayNumber> PlayAsync(PlayNumber playNumber);
    ValueTask<List<PlayNumber>> GetUserPlaysAsync(long userId);
    ValueTask SetWinningNumbersAsync(long numberId, int[] winningNumbers);

    ValueTask<Number> UpdateAsync(long id, Number number);
    ValueTask<bool> DeleteAsync(long id);
    ValueTask<Number> GetByIdAsync(long id);
    ValueTask<IEnumerable<Number>> GetAllAsync();
    ValueTask<IEnumerable<Number>> GetAllAsync(PaginationParams @params, Filter filter);
}