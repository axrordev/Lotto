using Lotto.Data.UnitOfWorks;
using Lotto.Domain.Entities;

namespace Lotto.Service.Services.CommentSettings;

public class CommentSettingService(IUnitOfWork unitOfWork) : ICommentSettingService
{
    public async ValueTask<int> GetCommentCooldownAsync()
    {
        var settings = await unitOfWork.CommentSettingRepository.SelectAsync(x => true);
        return settings?.CommentCooldown ?? 0; // ❗ Agar sozlama yo‘q bo‘lsa, default = 0
    }

    public async ValueTask SetCommentCooldownAsync(int cooldown)
    {
        var settings = await unitOfWork.CommentSettingRepository.SelectAsync(x => true);
        if (settings == null)
        {
            settings = new CommentSetting { CommentCooldown = cooldown };
            await unitOfWork.CommentSettingRepository.InsertAsync(settings);
        }
        else
        {
            settings.CommentCooldown = cooldown;
            await unitOfWork.CommentSettingRepository.UpdateAsync(settings);
        }
        await unitOfWork.SaveAsync();
    }
}