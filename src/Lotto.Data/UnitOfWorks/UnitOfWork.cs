using Lotto.Data.DbContexts;
using Lotto.Data.Repositories;
using Lotto.Domain.Entities;
using Lotto.Domain.Entities.Advertisements;
using Lotto.Domain.Entities.Games;

namespace Lotto.Data.UnitOfWorks;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext context;
    public UnitOfWork(AppDbContext context)
    {
        this.context = context;
        UserRepository = new Repository<User>(context);
        AssetRepository = new Repository<Asset>(context);
        AdvertisementRepository = new Repository<Advertisement>(context);
        AdvertisementViewRepository = new Repository<AdvertisementView>(context);
        FootballGameRepository = new Repository<FootballGame>(context);
        FootballGameResultRepository = new Repository<FootballGameResult>(context);
        GameRepository = new Repository<Game>(context);
        NumberGameRepository = new Repository<NumberGame>(context);
        PlayFootballRepository = new Repository<PlayFootball>(context);
        PlayNumberRepository = new Repository<PlayNumber>(context);
        ChatRepository = new Repository<Chat>(context);
        TransactionRepository = new Repository<Transaction>(context);
    }

    public IRepository<User> UserRepository { get; }
    public IRepository<Asset> AssetRepository { get; }
    public IRepository<Advertisement> AdvertisementRepository { get; }
    public IRepository<AdvertisementView> AdvertisementViewRepository { get; }
    public IRepository<FootballGame> FootballGameRepository { get; }
    public IRepository<FootballGameResult> FootballGameResultRepository { get; }
    public IRepository<Game> GameRepository { get; }
    public IRepository<NumberGame> NumberGameRepository { get; }
    public IRepository<PlayFootball> PlayFootballRepository { get; }
    public IRepository<PlayNumber> PlayNumberRepository { get; }
    public IRepository<Chat> ChatRepository { get; }
    public IRepository<Transaction> TransactionRepository { get; }





    public async ValueTask BeginTransactionAsync()
    {
        await context.Database.BeginTransactionAsync();
    }

    public async ValueTask CommitTransactionAsync()
    {
        await context.Database.CommitTransactionAsync();
    }

    public async ValueTask Rollback()
    {
        await context.Database.RollbackTransactionAsync();
    }

    public async ValueTask<bool> SaveAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }
}
