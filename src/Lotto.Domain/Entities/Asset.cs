using Lotto.Domain.Commons;

namespace Lotto.Domain.Entities;

public class Asset : Auditable
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
}
