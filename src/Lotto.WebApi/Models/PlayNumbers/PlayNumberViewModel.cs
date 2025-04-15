using Lotto.WebApi.Models.Numbers;
using Lotto.WebApi.Models.Users;

namespace Lotto.WebApi.Models.PlayNumbers
{
    public class PlayNumberViewModel
    {
        public long Id { get; set; }
        public UserViewModel User { get; set; }
        public NumberViewModel Number { get; set; }
        public int[] SelectedNumbers { get; set; }
        public bool IsWinner { get; set; }
    }
}
