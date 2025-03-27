
using FluentValidation;
using FluentValidation.Results;
using Lotto.Service.Exceptions;
using System.Linq;
using System.Threading.Tasks;
namespace Lotto.WebApi.Extensions;

public static class ValidationExtensions
{
    public static async Task<ValidationResult> EnsureValidatedAsync<TValidator, TObject>(this TValidator validator,
        TObject @object)
        where TObject : class
        where TValidator : AbstractValidator<TObject>
    {
        var validationResult = await validator.ValidateAsync(@object);
        if (validationResult.Errors.Any())
            throw new ArgumentIsNotValidException(validationResult.Errors.First().ErrorMessage);

        return validationResult;
    }

    public static async Task<ValidationResult> EnsureValidatedAsync(
        this IValidator<(string model1, string model2)> validator,
        string model1,
        string model2)
    {
        var tupleObject = (model1, model2);
        var validationResult = await validator.ValidateAsync(tupleObject);
        if (validationResult.Errors.Any())
            throw new ArgumentIsNotValidException(validationResult.Errors.First().ErrorMessage);

        return validationResult;
    }

    public static async Task<ValidationResult> EnsureValidatedAsync(
        this IValidator<(long model1, string model2)> validator,
        long model1,
        string model2)
    {
        var tupleObject = (model1, model2);
        var validationResult = await validator.ValidateAsync(tupleObject);
        if (validationResult.Errors.Any())
            throw new ArgumentIsNotValidException(validationResult.Errors.First().ErrorMessage);

        return validationResult;
    }
}