﻿ using Lotto.Data.Repositories;
using Lotto.Domain.Entities;
using Lotto.Domain.Entities.Advertisements;
using Lotto.Domain.Entities.Games;
using Lotto.Domain.Entities.Users;

namespace Lotto.Data.UnitOfWorks;

public interface IUnitOfWork : IDisposable
{
    IRepository<User> UserRepository { get; }
    IRepository<Asset> AssetRepository { get; }
    IRepository<Advertisement> AdvertisementRepository { get; }
    IRepository<Announcement> AnnouncementRepository { get; }
    IRepository<Football> FootballRepository { get; }
    IRepository<FootballResult> FootballResultRepository { get; }
    IRepository<Number> NumberRepository { get; }
    IRepository<PlayFootball> PlayFootballRepository { get; }
    IRepository<PlayNumber> PlayNumberRepository { get; }
    IRepository<Comment> CommentRepository { get; }
    IRepository<CommentSetting> CommentSettingRepository { get; }
    IRepository<UserRole> UserRoleRepository { get; }
    IRepository<UserRolePermission> UserRolePermissionRepository { get; }
    IRepository<Permission> PermissionRepository { get; }


    ValueTask<bool> SaveAsync();
    ValueTask BeginTransactionAsync();
    ValueTask CommitTransactionAsync();
    ValueTask Rollback();
}