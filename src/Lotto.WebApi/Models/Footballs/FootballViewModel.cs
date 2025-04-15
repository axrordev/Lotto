using System;

namespace Lotto.WebApi.Models.Footballs
{
    public class FootballViewModel
    {
        public long Id { get; set; }
        public string LigaName { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public DateTime MatchDay { get; set; }
        public string? FootballInfo { get; set; }
        public decimal Amount { get; set; }
        public bool IsCompleted { get; set; }
    }
}
