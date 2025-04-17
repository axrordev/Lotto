using System.Threading.Tasks;

namespace Lotto.WebApi.ApiServices.CommentServices;

public interface ICommentSettingApiService
{
    ValueTask<int> GetCommentCooldownAsync();
    ValueTask SetCommentCooldownAsync(int cooldown);
}
