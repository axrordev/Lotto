using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities;

public class Asset : Auditable
{
    public string Name { get; set; }
    public string Path { get; set; }
}
