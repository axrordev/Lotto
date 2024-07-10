 using Lotto.Data.Repositories;
using Lotto.Domain.Entities;
using Lotto.Domain.Entities.Advertisements;
using Lotto.Domain.Entities.Games;

namespace Lotto.Data.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> UserRepository { get; }
    IRepository<Asset> AssetRepository { get; }
    IRepository<Advertisement> AdvertisementRepository { get; }
    IRepository<AdvertisementView> AdvertisementViewRepository { get; }
    IRepository<FootballGame> FootballGameRepository { get; }
    IRepository<FootballGameResult> FootballGameResultRepository { get; }
    IRepository<Game> GameRepository { get; }
    IRepository<NumberGame> NumberGameRepository { get; }
    IRepository<PlayFootball> PlayFootballRepository { get; }
    IRepository<PlayNumber> PlayNumberRepository { get; }
    IRepository<Chat> ChatRepository { get; }
    IRepository<Transaction> TransactionRepository { get; }


    ValueTask<bool> SaveAsync();
    ValueTask BeginTransactionAsync();
    ValueTask CommitTransactionAsync();
    ValueTask Rollback();
}