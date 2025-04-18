﻿using System.Collections.Generic;

namespace Lotto.WebApi.Models.Footballs
{
    public class FootballResultViewModel
    {
        public long Id { get; set; }
        public FootballViewModel Football { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public List<GoalDetailViewModel> Goals { get; set; }
    }
}
