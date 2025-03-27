using Lotto.WebApi.Models.UserRoles;

namespace Lotto.WebApi.Models.Users;

public record UserViewModel(
    long Id,
    string Username,
    string Email,
    UserRoleViewModel Role);