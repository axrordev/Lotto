using Lotto.Domain.Entities;
using Lotto.Domain.Entities.Advertisements;
using Lotto.Domain.Entities.Games;
using Lotto.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Lotto.Data.DbContexts;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<AdvertisementView> AdvertisementViews { get; set; }
    public DbSet<Football> Footballs { get; set; }
    public DbSet<FootballResult> FootballResults { get; set; }
    public DbSet<Number> Numbers { get; set; }
    public DbSet<PlayFootball> PlayFootballs { get; set; }
    public DbSet<PlayNumber> PlayNumbers { get; set; }
    public DbSet<Asset> Assets { get; set; }
    public DbSet<Transaction> Transactions { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<UserRole> UsersRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Advertisement>().HasQueryFilter(ad => !ad.IsDeleted);
        modelBuilder.Entity<AdvertisementView>().HasQueryFilter(adView => !adView.IsDeleted);
        modelBuilder.Entity<Football>().HasQueryFilter(fg => !fg.IsDeleted);
        modelBuilder.Entity<FootballResult>().HasQueryFilter(fgr => !fgr.IsDeleted);
        modelBuilder.Entity<Number>().HasQueryFilter(ng => !ng.IsDeleted);
        modelBuilder.Entity<PlayFootball>().HasQueryFilter(pf => !pf.IsDeleted);
        modelBuilder.Entity<PlayNumber>().HasQueryFilter(pn => !pn.IsDeleted);
        modelBuilder.Entity<Asset>().HasQueryFilter(asset => !asset.IsDeleted);
        modelBuilder.Entity<Transaction>().HasQueryFilter(transaction => !transaction.IsDeleted);
        modelBuilder.Entity<User>().HasQueryFilter(user => !user.IsDeleted);
        modelBuilder.Entity<UserRole>().HasQueryFilter(userRole => !userRole.IsDeleted);

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
        modelBuilder.Entity<FootballResult>()
            .HasOne(fgr => fgr.Football)
            .WithMany()
            .HasForeignKey(fgr => fgr.FootballId);

        // FootballGameResult vs FootballGame 
        modelBuilder.Entity<FootballResult>()
            .HasOne(fgr => fgr.Football)
            .WithMany()
            .HasForeignKey(fgr => fgr.FootballId);

        // PlayFootball vs User 
        modelBuilder.Entity<PlayFootball>()
            .HasOne(pf => pf.User)
            .WithMany()
            .HasForeignKey(pf => pf.UserId);


        // PlayFootball vs FootballGame 
        modelBuilder.Entity<PlayFootball>()
            .HasOne(pf => pf.Football)
            .WithMany()
            .HasForeignKey(pf => pf.FootballId);

        // PlayNumber vs User 
        modelBuilder.Entity<PlayNumber>()
            .HasOne(pn => pn.User)
            .WithMany()
            .HasForeignKey(pn => pn.UserId);

        // PlayNumber vs NumberGame 
        modelBuilder.Entity<PlayNumber>()
            .HasOne(pn => pn.Number)
            .WithMany()
            .HasForeignKey(pn => pn.NumberId);

        // Transaction vs User 
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.User)
            .WithMany(u => u.Transactions)
            .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany()
            .HasForeignKey(t => t.RoleId);

        modelBuilder.Entity<PlayFootball>()
            .HasOne(pf => pf.User)
            .WithMany(u => u.PlayFootballs)
            .HasForeignKey(t => t.UserId);

        modelBuilder.Entity<PlayNumber>()
            .HasOne(pn => pn.User)
            .WithMany(u => u.PlayNumbers)
            .HasForeignKey(t => t.UserId);
        #endregion
    }
}
