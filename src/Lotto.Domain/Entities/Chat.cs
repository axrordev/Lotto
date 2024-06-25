using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities;

public class Chat : Auditable
{
    public string Info { get; set; }
    public long FileId { get; set; }
    public Asset File {  get; set; }
}