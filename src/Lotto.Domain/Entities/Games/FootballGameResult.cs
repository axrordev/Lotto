using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities.Games;

public class FootballGameResult : Auditable
{
    public long FootballGameId { get; set; }
    public FootballGame FootballGame { get; set; }
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }
    public List<int> GoalTimes { get; set; } = new List<int>();
}
                                                                                               