namespace Lotto.WebApi.Models.Footballs
{
    public class PlayFootballViewModel
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public long FootballId { get; set; }
        public int GoalTime { get; set; }
        public string ScoringPlayer { get; set; }
        public bool IsWinner { get; set; }
    }
}
