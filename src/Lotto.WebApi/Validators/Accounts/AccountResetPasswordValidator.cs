
using FluentValidation;
using Lotto.WebApi.Helpers;

namespace Lotto.WebApi.Validators.Accounts;

public class AccountResetPasswordValidator : AbstractValidator<(string email, string newPassword)>
{
    public AccountResetPasswordValidator()
    {
        RuleFor(model => model.email)
            .NotNull()
            .Must(ValidationHelper.IsValidEmail)
            .WithMessage("Email is not valid");

        RuleFor(model => model.newPassword)
            .NotNull()
            .Must(ValidationHelper.IsHardPassword)
            .WithMessage("Password is not valid, password must be hard!");
    }
}