using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities.Games;

public class FootballGame : Auditable
{
    public string LigaName { get; set; }
    public string HomeTeam { get; set; }
    public string AwayTeam { get; set; }
    public DateTime MatchDay { get; set; }
}
