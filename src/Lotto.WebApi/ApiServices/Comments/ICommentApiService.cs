using Lotto.Service.Configurations;
using Lotto.WebApi.Models.Comments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.Comments
{
public interface ICommentApiService
    {
        ValueTask<CommentViewModel> CreateAsync(long userId, CommentCreateModel createModel);
        ValueTask<CommentViewModel> UpdateAsync(long userId, long commentId, CommentUpdateModel updateModel);
        ValueTask<bool> DeleteAsync(long userId, long commentId);
        ValueTask<CommentViewModel> GetByIdAsync(long id);
        ValueTask<IEnumerable<CommentViewModel>> GetAllAsync(PaginationParams @params, Filter filter);
    }
}
