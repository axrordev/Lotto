using Lotto.Domain.Entities.Advertisements;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Lotto.WebApi.Models.Advertisements;

public class AdvertisementCreateModel
{
    public string? Title { get; set; }
    public string? Content { get; set; }
    public string? Url { get; set; }
    public DateTime? EndDate { get; set; }
}
