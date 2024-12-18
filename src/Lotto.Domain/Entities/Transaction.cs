using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities;

public class Transaction : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public bool IsSuccessful { get; set; } 
}