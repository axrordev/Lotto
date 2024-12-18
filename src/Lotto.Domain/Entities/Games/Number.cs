using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities.Games;

public class Number : Auditable
{
    public List<int> WinningNumbers { get; set; } = new List<int>();
    public TimeSpan Deadline { get; set; }
    public decimal Amount { get; set; }
    public bool IsCompleted { get; set; }
}