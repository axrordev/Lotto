
using Lotto.Data.UnitOfWorks;
using Lotto.Domain.Entities.Users;
using Lotto.Service.Configurations;
using Lotto.Service.Exceptions;
using Lotto.Service.Extensions;
using Lotto.Service.Helpers;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace Lotto.Service.Services.Permissions;

public class PermissionService(IUnitOfWork unitOfWork) : IPermissionService
{
    public async ValueTask<Permission> CreateAsync(Permission permission)
    {
        var existPermission = await unitOfWork.PermissionRepository
            .SelectAsync(p =>
                p.Controller.ToLower() == permission.Controller.ToLower() &&
                p.Action.ToLower() == permission.Action.ToLower());

        if (existPermission is not null)
            throw new AlreadyExistException("This permission is already exist");

        permission.CreatedById = HttpContextHelper.GetUserId;
        var createdPermission = await unitOfWork.PermissionRepository.InsertAsync(permission);
        await unitOfWork.SaveAsync();

        return createdPermission;
    }

    public async ValueTask<Permission> UpdateAsync(long id, Permission permission)
    {
        var existPermission = await unitOfWork.PermissionRepository.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"This permission is not found with this ID={id}");

        existPermission.Action = permission.Action;
        existPermission.Controller = permission.Controller;
        existPermission.UpdatedById = HttpContextHelper.GetUserId;

        var updatedPermission = await unitOfWork.PermissionRepository.UpdateAsync(existPermission);
        await unitOfWork.SaveAsync();

        return existPermission;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existPermission = await unitOfWork.PermissionRepository.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"This permission is not found with this ID={id}");

        await unitOfWork.PermissionRepository.DeleteAsync(existPermission);
        await unitOfWork.SaveAsync();
        return true;
    }

    public async ValueTask<Permission> GetByIdAsync(long id)
    {
        var existPermission = await unitOfWork.PermissionRepository.SelectAsync(p => p.Id == id)
            ?? throw new NotFoundException($"This permission is not found with this ID={id}");

        return existPermission;
    }

    public async ValueTask<IEnumerable<Permission>> GetAllAsync(PaginationParams @params, Filter filter)
    {
        var permissions = unitOfWork.PermissionRepository.SelectAsQueryable();

        return await permissions.ToPaginateAsQueryable(@params).OrderBy(filter).ToListAsync();
        
    }

    public async ValueTask<IPagedList<Permission>> GetAllAsync(int? page, string search = null)
    {
        var permissions = unitOfWork.PermissionRepository.SelectAsQueryable();

        return await permissions.ToPagedListAsync(page ?? 1, 20);
    }

    public async ValueTask<IEnumerable<Permission>> GetAllAsync()
    {
        return await unitOfWork.PermissionRepository.SelectAsEnumerableAsync();
    }
}