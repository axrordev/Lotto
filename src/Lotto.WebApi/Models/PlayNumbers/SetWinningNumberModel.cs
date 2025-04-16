namespace Lotto.WebApi.Models.PlayNumbers
{
    public class SetWinningNumbersModel
    {
        public long NumberId { get; set; }
        public int[] WinningNumbers { get; set; }
    }
}
