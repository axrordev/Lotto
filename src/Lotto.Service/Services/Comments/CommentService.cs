using Lotto.Data.UnitOfWorks;
using Lotto.Domain.Entities;
using Lotto.Domain.Entities.Users;
using Lotto.Service.Configurations;
using Lotto.Service.Exceptions;
using Lotto.Service.Extensions;
using Lotto.Service.Services.CommentSettings;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using static System.Net.Mime.MediaTypeNames;

namespace Lotto.Service.Services.Comments;

public class CommentService(IUnitOfWork unitOfWork, ICommentSettingService settingsService) : ICommentService
{
    public async ValueTask<Comment> CreateAsync(long userId, Comment comment)
    {
        var user = await unitOfWork.UserRepository.SelectAsync(u => u.Id == userId);
        if (user == null)
            throw new NotFoundException("User not found");

        int commentCooldown = await settingsService.GetCommentCooldownAsync();

        // ⏳ Admin belgilagan vaqtni tekshiramiz
        if (user.LastCommentTime.HasValue && commentCooldown > 0)
        {
            var elapsedSeconds = (DateTime.UtcNow - user.LastCommentTime.Value).TotalSeconds;
            if (elapsedSeconds < commentCooldown)
            {
                throw new InvalidOperationException($"You must wait {commentCooldown - (int)elapsedSeconds} seconds before sending another comment.");
            }
        }

        comment.UserId = userId;
        comment.CreatedAt = DateTime.UtcNow;

        await unitOfWork.CommentRepository.InsertAsync(comment);
        await unitOfWork.SaveAsync();

        // 🕒 Oxirgi comment vaqtini yangilaymiz
        user.LastCommentTime = DateTime.UtcNow;
        await unitOfWork.UserRepository.UpdateAsync(user);
        await unitOfWork.SaveAsync();

        return comment;
    }

    public async ValueTask<Comment> UpdateAsync(long userId, long commentId, Comment comment)
    {
        var existingComment = await unitOfWork.CommentRepository.SelectAsync(c => c.Id == commentId);
        if (existingComment == null)
            throw new NotFoundException("Comment not found");

        // 🛑 Foydalanuvchi faqat o‘z izohini tahrirlashi mumkin
        if (existingComment.UserId != userId)
            throw new UnauthorizedAccessException("You can only edit your own comments.");

        existingComment.Text = comment.Text;
        existingComment.UpdatedAt = DateTime.UtcNow;

        await unitOfWork.CommentRepository.UpdateAsync(existingComment);
        await unitOfWork.SaveAsync();

        return existingComment;
    }

    public async ValueTask<bool> DeleteAsync(long userId, long commentId)
    {
        var comment = await unitOfWork.CommentRepository.SelectAsync(c => c.Id == commentId);
        if (comment == null)
            throw new NotFoundException("Comment not found");

        // 🛑 Foydalanuvchi faqat o‘z izohini o‘chirishi mumkin
        if (comment.UserId != userId)
            throw new UnauthorizedAccessException("You can only delete your own comments.");

        await unitOfWork.CommentRepository.DeleteAsync(comment);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<Comment> GetByIdAsync(long id)
    {
        var comment = await unitOfWork.CommentRepository.SelectAsync(c => c.Id == id, includes: ["User"]);
        if (comment == null)
            throw new NotFoundException("Comment not found");

        return comment;
    }

    public async ValueTask<IEnumerable<Comment>> GetAllAsync(PaginationParams @params, Filter filter)
    {
       var comments = unitOfWork.CommentRepository
        .SelectAsQueryable(includes: ["User"])
        .OrderBy(filter) 
        .ToPaginateAsQueryable(@params); 

        return await comments.ToListAsync(); 
    }
}
