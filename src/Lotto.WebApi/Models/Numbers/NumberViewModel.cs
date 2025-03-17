using System;

namespace Lotto.WebApi.Models.Numbers
{
    public class NumberViewModel
    {
        public long Id { get; set; }
        public DateTime Deadline { get; set; }
        public decimal Amount { get; set; }
        public bool IsCompleted { get; set; }
    }
}
