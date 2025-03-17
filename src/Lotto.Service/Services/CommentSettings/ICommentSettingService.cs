using Lotto.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotto.Service.Services.CommentSettings;

public interface ICommentSettingService
{
    ValueTask<int> GetCommentCooldownAsync();
    ValueTask SetCommentCooldownAsync(int cooldown);
}
