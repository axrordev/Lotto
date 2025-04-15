using Lotto.Domain.Entities.Advertisements;
using System.Collections.Generic;
using System;

namespace Lotto.WebApi.Models.Advertisements;

public class AdvertisementUpdateModel
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Url { get; set; }
    public DateTime EndDate { get; set; }
}