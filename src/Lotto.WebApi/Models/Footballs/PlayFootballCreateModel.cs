namespace Lotto.WebApi.Models.Footballs
{
    public class PlayFootballCreateModel
    {
        public long UserId { get; set; }
        public long FootballId { get; set; }
        public int GoalTime { get; set; }
        public string ScoringPlayer { get; set; }
    }
}
