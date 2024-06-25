using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities.Advertisements;

public class AdvertisementView : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long AdvertisementId { get; set; }
    public Advertisement Advertisement { get; set; }
    public DateTime ViewTimestamp { get; set; }
}
