using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities;

public class Announcement : Auditable
{
    public string Title { get; set; }  
    public string Message { get; set; } 
    public DateTime ExpiryDate { get; set; }  
    public bool IsActive { get; set; }  
}
