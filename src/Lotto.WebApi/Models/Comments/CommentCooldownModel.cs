using System.ComponentModel.DataAnnotations;

namespace Lotto.WebApi.Models.Comments;

public class CommentCooldownModel
{
    [Required(ErrorMessage = "Cooldown is required")]
    [Range(0, 1440, ErrorMessage = "Cooldown must be between 0 and 1440 minutes (1 day)")]
    public int Cooldown { get; set; } // Minutda kiritiladi
}