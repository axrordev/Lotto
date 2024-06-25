using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities.Advertisements;

public class Advertisement : Auditable
{
    public long FileId { get; set; }
    public Asset File { get; set; }
    public string Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
