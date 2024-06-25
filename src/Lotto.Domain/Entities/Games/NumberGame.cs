using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities.Games;

public class NumberGame : Auditable
{
    public int TotalCountOfNumbers { get; set; }
    public List<int> WinningNumbers { get; set; } = new List<int>();
    public TimeSpan Deadline { get; set; }
}
