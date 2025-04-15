using Lotto.Data.DbContexts;
using Lotto.Data.Repositories;
using Lotto.Domain.Entities;
using Lotto.Domain.Entities.Advertisements;
using Lotto.Domain.Entities.Games;
using Lotto.Domain.Entities.Users;

namespace Lotto.Data.UnitOfWorks;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    private readonly AppDbContext context = context;

    public IRepository<User> UserRepository { get; } = new Repository<User>(context);
    public IRepository<Asset> AssetRepository { get; } = new Repository<Asset>(context);
    public IRepository<Advertisement> AdvertisementRepository { get; } = new Repository<Advertisement>(context);
    public IRepository<Announcement> AnnouncementRepository { get; } = new Repository<Announcement>(context);
    public IRepository<Football> FootballRepository { get; } = new Repository<Football>(context);
    public IRepository<FootballResult> FootballResultRepository { get; } = new Repository<FootballResult>(context);
    public IRepository<Number> NumberRepository { get; } = new Repository<Number>(context);
    public IRepository<PlayFootball> PlayFootballRepository { get; } = new Repository<PlayFootball>(context);
    public IRepository<PlayNumber> PlayNumberRepository { get; } = new Repository<PlayNumber>(context);
    public IRepository<Comment> CommentRepository { get; } = new Repository<Comment>(context);
    public IRepository<CommentSetting> CommentSettingRepository { get; } = new Repository<CommentSetting>(context);
    public IRepository<Permission> PermissionRepository { get; } = new Repository<Permission>(context);
    public IRepository<UserRole> UserRoleRepository { get; } = new Repository<UserRole>(context);
    public IRepository<UserRolePermission> UserRolePermissionRepository { get; } = new Repository<UserRolePermission>(context);


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
