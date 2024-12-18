using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities.Games;

public class Football : Auditable
{
    public string LigaName { get; set; }
    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }
    public DateTime MatchDay { get; set; }
    public decimal Amount { get; set; }
    public bool IsCompleted { get; set; }
}
