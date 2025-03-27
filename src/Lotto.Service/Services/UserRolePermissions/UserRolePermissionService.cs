using Lotto.Data.UnitOfWorks;
using Lotto.Domain.Entities.Users;
using Lotto.Service.Configurations;
using Lotto.Service.Exceptions;
using Lotto.Service.Extensions;
using Lotto.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Lotto.Service.Services.UserRolePermissions;

public class UserRolePermissionService(IUnitOfWork unitOfWork) : IUserRolePermissionService
{
    public async ValueTask<UserRolePermission> CreateAsync(UserRolePermission userRolePermission)
    {
        var alreadyExistUserRolePermission = await unitOfWork.UserRolePermissionRepository
               .SelectAsync(urp =>
                    urp.UserRoleId == userRolePermission.UserRoleId &&
                    urp.PermissionId == userRolePermission.PermissionId);
        if (alreadyExistUserRolePermission is not null)
            throw new AlreadyExistException($"This User Role Permission is already exist with this Id={userRolePermission.Id}");

        var existUserRole = await unitOfWork.UserRoleRepository
            .SelectAsync(r => r.Id == userRolePermission.UserRoleId)
            ?? throw new NotFoundException("User role is not found!");

        var existPermission = await unitOfWork.PermissionRepository
            .SelectAsync(p => p.Id == userRolePermission.PermissionId)
            ?? throw new NotFoundException("Permission is not found!");

        userRolePermission.CreatedById = HttpContextHelper.GetUserId;
        var createdUserRolePermission = await unitOfWork.UserRolePermissionRepository.InsertAsync(userRolePermission);
        await unitOfWork.SaveAsync();
        return createdUserRolePermission;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existUserRolePermission = await unitOfWork.UserRolePermissionRepository.SelectAsync(urp => urp.Id == id)
            ?? throw new NotFoundException($"This User Role Permission is not found with this ID={id}");

        await unitOfWork.UserRolePermissionRepository.DeleteAsync(existUserRolePermission);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<IEnumerable<UserRolePermission>> GetAlByRoleIdAsync(long roleId)
    {
        var existUserRole = await unitOfWork.UserRoleRepository
            .SelectAsync(expression: r => r.Id == roleId)
            ?? throw new NotFoundException("User role is not found!");

        return await unitOfWork.UserRolePermissionRepository
            .SelectAsEnumerableAsync(expression: p => p.UserRoleId == roleId, includes: ["UserRole", "Permission"]);
    }

    public async ValueTask<IEnumerable<UserRolePermission>> GetAllAsync(PaginationParams @params, Filter filter)
    {
        var userRolePermissions = unitOfWork.UserRolePermissionRepository
            .SelectAsQueryable(includes: ["UserRole", "Permission"]);

        return await userRolePermissions.ToPaginateAsQueryable(@params).OrderBy(filter).ToListAsync();
        
    }

    public async ValueTask<IEnumerable<UserRolePermission>> GetAllAsync()
    {
        return await unitOfWork.UserRolePermissionRepository.SelectAsEnumerableAsync(includes: ["Permission", "UserRole"]);
    }

    public async ValueTask<UserRolePermission> GetByIdAsync(long id)
    {
        var existUserRolePermission = await unitOfWork.UserRolePermissionRepository
            .SelectAsync(expression: urp => urp.Id == id, includes: ["UserRole", "Permission"])
          ?? throw new NotFoundException($"This User Role Permission is not found with this ID={id}");

        return existUserRolePermission;
    }

    public async ValueTask<UserRolePermission> UpdateAsync(long id, UserRolePermission userRolePermission)
    {
        var existUserRolePermission = await unitOfWork.UserRolePermissionRepository.SelectAsync(urp => urp.Id == id)
               ?? throw new NotFoundException($"This User Role Permission is not found with this ID={id}");

        var existUserRole = await unitOfWork.UserRoleRepository
            .SelectAsync(r => r.Id == userRolePermission.UserRoleId)
            ?? throw new NotFoundException("User role is not found!");

        var existPermission = await unitOfWork.PermissionRepository
            .SelectAsync(p => p.Id == userRolePermission.PermissionId)
            ?? throw new NotFoundException("Permission is not found!");

        var alreadyExistUserRolePermission = await unitOfWork.UserRolePermissionRepository
            .SelectAsync(urp => urp.Id != id && urp.UserRoleId == userRolePermission.UserRoleId && urp.PermissionId == userRolePermission.PermissionId);
        if (alreadyExistUserRolePermission is not null)
            throw new AlreadyExistException($"This User Role is already exist with this dates = {userRolePermission.UserRoleId} and {userRolePermission.PermissionId}");

        existUserRolePermission.UserRoleId = userRolePermission.UserRoleId;
        existUserRolePermission.PermissionId = userRolePermission.PermissionId;

        var updatedUserRolePermission = await unitOfWork.UserRolePermissionRepository.UpdateAsync(existUserRolePermission);
        await unitOfWork.SaveAsync();
        return updatedUserRolePermission;
    }
}