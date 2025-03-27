
using Lotto.Data.UnitOfWorks;
using Lotto.Domain.Entities.Users;
using Lotto.Service.Exceptions;
using Lotto.Service.Helpers;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;

namespace Lotto.Service.Services.Accounts;

public class AccountService(IUnitOfWork unitOfWork, IMemoryCache memoryCache) : IAccountService
{
    public async ValueTask RegisterAsync(User user)
    {
        var existUser = await unitOfWork.UserRepository.SelectAsync(u => u.Email == user.Email);

        if (existUser?.Email is not null)
        {
            throw new AlreadyExistException($"This user is already exist with this email | Email={user.Email}");
        }

        var roleWhichIsUser = await unitOfWork.UserRoleRepository.SelectAsync(u => u.Name.ToLower() == "user");

        user.RoleId = roleWhichIsUser.Id;
        user.Password = PasswordHasher.Hash(user.Password);
        var json = JsonConvert.SerializeObject(user);
        CacheSet($"registerKey-{user.Email}", json);

        await EmailHelper.SendCodeAsync(memoryCache, user.Email, $"registerCodeKey-{user.Email}");
    }

    public async ValueTask RegisterVerifyAsync(string email, string code)
    {
        var codeInCache = memoryCache.Get($"registerCodeKey-{email}");
        if (codeInCache?.ToString() != code)
            throw new ArgumentIsNotValidException("Invalid code");

        CacheSet($"verifiedAccount-{email}", "verified");

        await Task.CompletedTask;
    }

    public async ValueTask<User> CreateAsync(string email)
    {
        var cacheValue = memoryCache.Get($"verifiedAccount-{email}");
        if (cacheValue is null)
            throw new ArgumentIsNotValidException("Account is not verified");

        var json = memoryCache.Get($"registerKey-{email}");
        var user = JsonConvert.DeserializeObject<User>(json.ToString());

        var createdUser = await unitOfWork.UserRepository.InsertAsync(user);
        await unitOfWork.SaveAsync();

        return createdUser;
    }

    public async ValueTask<(User user, string token)> LoginAsync(string email, string password)
    {
        if (EnvironmentHelper.SuperAdminLogin == email && EnvironmentHelper.SuperAdminPassword == password)
        {
            var superAdminRole = await unitOfWork.UserRoleRepository
                .SelectAsync(role => role.Name.ToLower() == "superadmin");

            var user = new User
            {
                Id = -1,
                Role = superAdminRole,
                RoleId = superAdminRole.Id,
                Email = EnvironmentHelper.SuperAdminLogin
            };
            return (user, token: AuthHelper.GenerateToken(-1, EnvironmentHelper.SuperAdminLogin, "SuperAdmin"));
        }
        else
        {
            var existUser = await unitOfWork.UserRepository
                .SelectAsync(expression: user => user.Email == email, includes: ["Role"])
            ?? throw new ForbiddenException("Email or Password is invalid");

            if (!PasswordHasher.Verify(password, existUser.Password))
                throw new ForbiddenException("Email or Password is invalid");

            return (user: existUser, token: AuthHelper.GenerateToken(existUser.Id, existUser.Email, existUser.Role.Name));
        }
    }

    public async ValueTask<bool> SendCodeAsync(string email)
    {
        var existUser = await unitOfWork.UserRepository.SelectAsync(user => user.Email == email)
            ?? throw new ForbiddenException("This user is not found");

        await EmailHelper.SendCodeAsync(memoryCache, existUser.Email, $"codeKey-{email}");

        return true;
    }

    public async ValueTask<bool> VerifyAsync(string email, string code)
    {
        var existUser = await unitOfWork.UserRepository.SelectAsync(user => user.Email == email)
            ?? throw new ForbiddenException("This user is not found");

        var codeInCache = memoryCache.Get($"codeKey-{email}");
        if (codeInCache.ToString() != code)
            throw new ArgumentIsNotValidException("Code is invalid");

        CacheSet($"verified-{email}", "verified");

        return true;
    }

    public async ValueTask<User> ResetPasswordAsync(string email, string newPassword)
    {
        var existUser = await unitOfWork.UserRepository.SelectAsync(user => user.Email == email)
            ?? throw new ForbiddenException("This user is not found");

        var cacheValue = memoryCache.Get($"verified-{existUser.Email}");
        if (cacheValue is null)
            throw new ArgumentIsNotValidException("Account is not verified");

        existUser.Password = PasswordHasher.Hash(newPassword);
        await unitOfWork.UserRepository.UpdateAsync(existUser);
        await unitOfWork.SaveAsync();

        memoryCache.Remove($"verified-{existUser.Email}");

        return existUser;
    }

    private void CacheSet(string key, string value)
    {
        var cacheOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromSeconds(60))
                .SetAbsoluteExpiration(TimeSpan.FromSeconds(3600))
                .SetPriority(CacheItemPriority.Normal)
                .SetSize(1024);

        memoryCache.Set(key, value, cacheOptions);
    }
}