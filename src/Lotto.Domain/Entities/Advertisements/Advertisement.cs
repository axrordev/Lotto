using Lotto.Domain.Commons;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace Lotto.Domain.Entities.Advertisements;

public class Advertisement : Auditable
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Url { get; set; }
    public string FileUrl { get; set; } // Yuklangan video yoki GIF fayl manzili
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }      
    public ICollection<AdvertisementView> Views { get; set; } = new List<AdvertisementView>();
}