using Lotto.Service.Services.CommentSettings;
using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.CommentServices
{
public class CommentSettingApiService(ICommentSettingService _commentSettingService) : ICommentSettingApiService
    {
        public async ValueTask<int> GetCommentCooldownAsync()
        {
            return await _commentSettingService.GetCommentCooldownAsync();
        }

        public async ValueTask SetCommentCooldownAsync(int cooldown)
        {
            await _commentSettingService.SetCommentCooldownAsync(cooldown);
        }
    }
}
