using System;

namespace Lotto.WebApi.Models.Numbers
{
    public class NumberCreateModel
    {
        public string WinningNumbers { get; set; }
        public DateTime Deadline { get; set; }
        public decimal Amount { get; set; }
    }
}
