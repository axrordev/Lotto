using Lotto.Domain.Entities.Advertisements;
using Lotto.Domain.Entities.Users;
using Lotto.WebApi.Models.Advertisements;
using System;

namespace Lotto.WebApi.Models.AdvertisementViews;

public class AdvertisementViewViewModel
{
    public long Id { get; set; }
    public User User { get; set; }
    public AdvertisementViewModel Advertisement { get; set; }
    public DateTime ViewedAt { get; set; }
}
