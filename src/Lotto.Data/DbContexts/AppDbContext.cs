using Lotto.Domain.Entities;
using Lotto.Domain.Entities.Advertisements;
using Lotto.Domain.Entities.Games;
using Microsoft.EntityFrameworkCore;

namespace Lotto.Data.DbContexts;

public class AppDbContext : DbContext
{
    //public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)  {}

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql("Server=localhost; Database=Lotto; Port=5432; User ID=postgres; Password=1001");
    }

    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<AdvertisementView> AdvertisementViews { get; set; }
    public DbSet<FootballGame> FootballGames { get; set; }
    public DbSet<FootballGameResult> FootballGameResults { get; set; }
    public DbSet<Game> Games { get; set; }
    public DbSet<NumberGame> NumberGames { get; set; }
    public DbSet<PlayFootball> PlayFootballs { get; set; }
    public DbSet<PlayNumber> PlayNumbers { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Chat> Chats { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserAccount> UserAccounts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Advertisement>().HasQueryFilter(ad => !ad.IsDeleted);
        modelBuilder.Entity<AdvertisementView>().HasQueryFilter(adView => !adView.IsDeleted);
        modelBuilder.Entity<FootballGame>().HasQueryFilter(fg => !fg.IsDeleted);
        modelBuilder.Entity<FootballGameResult>().HasQueryFilter(fgr => !fgr.IsDeleted);
        modelBuilder.Entity<Game>().HasQueryFilter(game => !game.IsDeleted);
        modelBuilder.Entity<NumberGame>().HasQueryFilter(ng => !ng.IsDeleted);
        modelBuilder.Entity<PlayFootball>().HasQueryFilter(pf => !pf.IsDeleted);
        modelBuilder.Entity<PlayNumber>().HasQueryFilter(pn => !pn.IsDeleted);
        modelBuilder.Entity<Asset>().HasQueryFilter(asset => !asset.IsDeleted);
        modelBuilder.Entity<Chat>().HasQueryFilter(chat => !chat.IsDeleted);
        modelBuilder.Entity<Transaction>().HasQueryFilter(transaction => !transaction.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(user => !user.IsDeleted);
        modelBuilder.Entity<UserAccount>().HasQueryFilter(userAccount => !userAccount.IsDeleted);

        #region FluentApi
        // Ad vs File 
        modelBuilder.Entity<Advertisement>()
            .HasOne(ad => ad.File)
            .WithMany()
            .HasForeignKey(ad => ad.FileId);

        // Ad vs AdView 
        modelBuilder.Entity<AdvertisementView>()
            .HasOne(adView => adView.Advertisement)
            .WithMany()
            .HasForeignKey(adView => adView.AdvertisementId);

        // AdView vs User 
        modelBuilder.Entity<AdvertisementView>()
            .HasOne(adView => adView.User)
            .WithMany()
            .HasForeignKey(adView => adView.UserId);

        // FootballGameResult vs FootballGame 
        modelBuilder.Entity<FootballGameResult>()
            .HasOne(fgr => fgr.FootballGame)
            .WithMany()
            .HasForeignKey(fgr => fgr.FootballGameId);

        // FootballGameResult vs FootballGame 
        modelBuilder.Entity<FootballGameResult>()
            .HasOne(fgr => fgr.FootballGame)
            .WithMany()
            .HasForeignKey(fgr => fgr.FootballGameId);

        // Game vs Image 
        modelBuilder.Entity<Game>()
            .HasOne(game => game.Image)
            .WithMany()
            .HasForeignKey(game => game.ImageId);

        // PlayFootball vs User 
        modelBuilder.Entity<PlayFootball>()
            .HasOne(pf => pf.User)
            .WithMany()
            .HasForeignKey(pf => pf.UserId);


        // PlayFootball vs FootballGame 
        modelBuilder.Entity<PlayFootball>()
            .HasOne(pf => pf.FootballGame)
            .WithMany()
            .HasForeignKey(pf => pf.FootballGameId);

        // PlayNumber vs User 
        modelBuilder.Entity<PlayNumber>()
            .HasOne(pn => pn.User)
            .WithMany()
            .HasForeignKey(pn => pn.UserId);

        // PlayNumber vs NumberGame 
        modelBuilder.Entity<PlayNumber>()
            .HasOne(pn => pn.NumberGame)
            .WithMany()
            .HasForeignKey(pn => pn.NumberGameId);

        // Chat vs File 
        modelBuilder.Entity<Chat>()
            .HasOne(chat => chat.File)
            .WithMany()
            .HasForeignKey(chat => chat.FileId);

        // Transaction vs User 
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.UserId);

        // User vs UserAccount
        modelBuilder.Entity<User>()
            .HasOne(u => u.UserAccount)
            .WithMany()
            .HasForeignKey(t => t.UserAccountId);


        #endregion
    }
}
