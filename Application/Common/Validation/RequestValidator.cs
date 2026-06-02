using Application.Exceptions;
using FluentValidation;

namespace Application.Common.Validation;

public class RequestValidator(IServiceProvider serviceProvider) : IRequestValidator
{
    public async Task ValidateAsync<T>(T request)
    {
        var validator = serviceProvider.GetService(typeof(IValidator<T>)) as IValidator<T>;
        if (validator is null)
            return;

        var validationResult = await validator.ValidateAsync(request);

        if (!validationResult.IsValid)
        {
            var errors = validationResult.Errors
                .GroupBy(x => x.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(x => x.ErrorMessage).ToArray());

            throw new CustomValidationException(errors);
        }
    }
}