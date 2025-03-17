using Lotto.Domain.Entities;
using Lotto.Service.Configurations;

namespace Lotto.Service.Services.Comments;

public interface ICommentService
{
    ValueTask<Comment> CreateAsync(long userId, Comment comment);
    ValueTask<Comment> UpdateAsync(long userId, long commentId, Comment comment);
    ValueTask<bool> DeleteAsync(long userId, long commentId);
    ValueTask<Comment> GetByIdAsync(long id);
    ValueTask<IEnumerable<Comment>> GetAllAsync(PaginationParams @params, Filter filter);
}
