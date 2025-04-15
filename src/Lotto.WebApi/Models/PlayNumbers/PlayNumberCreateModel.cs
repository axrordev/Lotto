namespace Lotto.WebApi.Models.PlayNumbers
{
    public class PlayNumberCreateModel
    {
        public long UserId { get; set; }
        public long NumberId { get; set; }
        public int[] SelectedNumbers { get; set; } // [5, 12, 23, 34, 45]
    }
}
