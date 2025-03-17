using Lotto.Domain.Commons;
using Lotto.Domain.Entities.Users;

namespace Lotto.Domain.Entities.Advertisements;

public class AdvertisementView : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }
    public long AdvertisementId { get; set; }
    public Advertisement Advertisement { get; set; }
    public DateTime ViewedAt { get; set; }
}
