using Lotto.Domain.Entities.Advertisements;
using System.Collections.Generic;
using System;
using Microsoft.AspNetCore.Http;

namespace Lotto.WebApi.Models.Advertisements;

public class AdvertisementViewModel
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Url { get; set; }
    public string FileUrl { get; set; }// Yuklangan video yoki GIF fayl manzili
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }      
}
