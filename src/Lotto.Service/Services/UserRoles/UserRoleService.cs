
using Lotto.Data.UnitOfWorks;
using Lotto.Domain.Entities.Users;
using Lotto.Service.Configurations;
using Lotto.Service.Exceptions;
using Lotto.Service.Extensions;
using Lotto.Service.Helpers;
using Microsoft.EntityFrameworkCore;

namespace Lotto.Service.Services.UserRoles;

public class UserRoleService(IUnitOfWork unitOfWork) : IUserRoleService
{
    public async ValueTask<UserRole> CreateAsync(UserRole userRole)
    {
        var existUserRole = await unitOfWork.UserRoleRepository.SelectAsync(uRole => uRole.Name.ToLower() == userRole.Name.ToLower());

        if (existUserRole != null)
            throw new AlreadyExistException("UserRole is already exist");

        userRole.CreatedById = HttpContextHelper.GetUserId;
        var createdUserRole = await unitOfWork.UserRoleRepository.InsertAsync(userRole);
        await unitOfWork.SaveAsync();

        return createdUserRole;
    }

    public async ValueTask<UserRole> UpdateAsync(long id, UserRole userRole)
    {
        var existUserRole = await unitOfWork.UserRoleRepository.SelectAsync(uRole => uRole.Id == id)
            ?? throw new NotFoundException($"This user role is not found with this ID={id}");

        existUserRole.Name = userRole.Name;
        await unitOfWork.UserRoleRepository.UpdateAsync(existUserRole);
        await unitOfWork.SaveAsync();

        return existUserRole;
    }

    public async ValueTask<bool> DeleteAsync(long id)
    {
        var existUserRole = await unitOfWork.UserRoleRepository.SelectAsync(uRole => uRole.Id == id)
            ?? throw new NotFoundException($"This user role is not found with this ID={id}");

        await unitOfWork.UserRoleRepository.DeleteAsync(existUserRole);
        await unitOfWork.SaveAsync();

        return true;
    }

    public async ValueTask<IEnumerable<UserRole>> GetAllAsync(PaginationParams @params, Filter filter)
    {
        var userRoles = unitOfWork.UserRoleRepository.SelectAsQueryable();

        var pagedUserRoles = userRoles.ToPaginateAsQueryable(@params).OrderBy(filter);
        return await pagedUserRoles.ToListAsync();
    }
    public async ValueTask<IEnumerable<UserRole>> GetAllAsync()
    {
        return await unitOfWork.UserRoleRepository.SelectAsEnumerableAsync();
    }

    public async ValueTask<UserRole> GetByIdAsync(long id)
    {
        var existUserRole = await unitOfWork.UserRoleRepository.SelectAsync(uRole => uRole.Id == id)
            ?? throw new NotFoundException($"This user role is not found with this ID={id}");

        return existUserRole;
    }

    public async ValueTask<UserRole> GetByNameAsync(string name)
    {
        var existUserRole = await unitOfWork.UserRoleRepository
            .SelectAsync(role => role.Name.ToLower() == name.ToLower())
           ?? throw new NotFoundException($"This user role is not found with this name={name}");

        return existUserRole;
    }
}
