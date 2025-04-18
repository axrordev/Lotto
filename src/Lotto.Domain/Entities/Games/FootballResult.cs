﻿using Lotto.Domain.Commons;

namespace Lotto.Domain.Entities.Games;

public class FootballResult : Auditable
{
    public long FootballId { get; set; }
    public Football Football { get; set; }
    public int HomeTeamScore { get; set; }
    public int AwayTeamScore { get; set; }
    public List<GoalDetail> Goals { get; set; } = new List<GoalDetail>();
}
                                                                                               