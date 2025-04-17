using Lotto.WebApi.Models.Users;

namespace Lotto.WebApi.Models.Footballs
{
    public class PlayFootballViewModel
    {
        public long Id { get; set; }
        public UserViewModel User { get; set; }
        public FootballViewModel Football { get; set; }
        public int GoalTime { get; set; }
        public string ScoringPlayer { get; set; }
        public bool IsWinner { get; set; }
    }
}
