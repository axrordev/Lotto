using AutoMapper;
using Lotto.Domain.Entities;
using Lotto.Service.Configurations;
using Lotto.Service.Services.Comments;
using Lotto.WebApi.Models.Comments;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.Comments
{
public class CommentApiService(ICommentService _commentService,IMapper _mapper) : ICommentApiService
    {
        public async ValueTask<CommentViewModel> CreateAsync(long userId, CommentCreateModel createModel)
        {
            var comment = _mapper.Map<Comment>(createModel);
            var createdComment = await _commentService.CreateAsync(userId, comment);
            return _mapper.Map<CommentViewModel>(createdComment);
        }

        public async ValueTask<CommentViewModel> UpdateAsync(long userId, long commentId, CommentUpdateModel updateModel)
        {
            var comment = _mapper.Map<Comment>(updateModel);
            var updatedComment = await _commentService.UpdateAsync(userId, commentId, comment);
            return _mapper.Map<CommentViewModel>(updatedComment);
        }

        public async ValueTask<bool> DeleteAsync(long userId, long commentId)
        {
            return await _commentService.DeleteAsync(userId, commentId);
        }

        public async ValueTask<CommentViewModel> GetByIdAsync(long id)
        {
            var comment = await _commentService.GetByIdAsync(id);
            return _mapper.Map<CommentViewModel>(comment);
        }

        public async ValueTask<IEnumerable<CommentViewModel>> GetAllAsync(PaginationParams @params, Filter filter)
        {
            var comments = await _commentService.GetAllAsync(@params, filter);
            return _mapper.Map<IEnumerable<CommentViewModel>>(comments);
        }
    }
}
