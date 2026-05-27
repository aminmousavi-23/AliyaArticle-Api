using Application.Common.Exceptions;

namespace Application.Common.Validaton;

public class RequestValidator : IRequestValidator
{
    public async Task ValidateAsync<TRequest, TValidator>(TRequest request)
        where TValidator : AbstractValidator<TRequest>, new()
    {
        var validator = new TValidator();
        var validationResult = await validator.ValidateAsync(request);

        if (validationResult.IsValid == false)
        {
            var errors = validationResult.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
            throw new CustomValidationException(errors);
        }
    }
}