using System;
using Lotto.WebApi.Models.Numbers;
using Lotto.WebApi.Models.Commons;
namespace Lotto.WebApi.Models.Numbers
{
    public class NumberCreateModel
    {
        public int[] WinningNumbers { get; set; }
        public DateTime Deadline { get; set; }
        public decimal Amount { get; set; }
    }
}
