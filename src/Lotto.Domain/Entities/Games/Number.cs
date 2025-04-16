using Lotto.Domain.Commons;

namespace Lotto.Domain.Entities.Games;

public class Number : Auditable
{
    public int[]? WinningNumbers { get; set; }
    public DateTime Deadline { get; set; }
    public decimal Amount { get; set; }
    public bool IsCompleted { get; set; }
}