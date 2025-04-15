using Lotto.Domain.Commons;

namespace Lotto.Domain.Entities.Games;

public class Number : Auditable
{
    public string EncryptedWinningNumbers { get; set; } // Shifrlangan winning numbers
    public string WinningNumbersHash { get; set; }
    public DateTime Deadline { get; set; }
    public decimal Amount { get; set; }
    public bool IsCompleted { get; set; }
}