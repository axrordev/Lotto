using Lotto.Domain.Commons;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lotto.Domain.Entities;

public class CommentSetting : Auditable
{
    public int CommentCooldown { get; set; } = 0; // 🕒 Admin belgilaydi (soniyalarda)
}

