using Lotto.WebApi.Models.Permissions;
using System.Collections.Generic;

namespace Lotto.WebApi.Models.Users;

public class LoginViewModel
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string Token { get; set; }
    public IEnumerable<PermissionViewModel> Permissions { get; set; }
}

