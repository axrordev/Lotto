using Lotto.Domain.Entities.Users;
using Lotto.WebApi.Models.Users;

namespace Lotto.WebApi.Models.Comments
{
    public class CommentViewModel
    {
        public long Id { get; set; }
        public UserViewModel User { get; set; }  
        public string Text { get; set; }
    }
}
