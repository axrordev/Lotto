using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities.Advertisements;

public class Advertisement : Auditable
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Url { get; set; }
    public string FileUrl { get; set; } // Yuklangan video yoki GIF fayl manzili
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}