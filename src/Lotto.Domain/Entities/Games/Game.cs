using Telegram.Domain.Commons;

namespace Lotto.Domain.Entities.Games;

public  class Game : Auditable
{
    public string Name { get; set; }
    public long ImageId { get; set; }
    public Asset Image { get; set; }
}