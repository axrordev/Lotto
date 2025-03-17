using Lotto.Domain.Commons;
using Lotto.Domain.Entities.Users;


namespace Lotto.Domain.Entities;

public class Comment : Auditable
{
    public long UserId { get; set; }
    public User User { get; set; }  
    public string Text { get; set; }
}
     