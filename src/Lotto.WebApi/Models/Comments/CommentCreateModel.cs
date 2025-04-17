using Lotto.Domain.Entities.Users;

namespace Lotto.WebApi.Models.Comments
{
    public class CommentCreateModel
    {
        public long UserId { get; set; }
        public User User { get; set; }  
        public string Text { get; set; }
    }
}
