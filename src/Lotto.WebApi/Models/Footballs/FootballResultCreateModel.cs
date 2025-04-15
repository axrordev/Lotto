using System.Collections.Generic;

namespace Lotto.WebApi.Models.Footballs
{
    public class FootballResultCreateModel
    {
        public long FootballId { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public List<GoalDetailCreateModel> Goals { get; set; }
    }
}
