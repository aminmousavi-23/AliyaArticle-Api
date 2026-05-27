using Application.Common.Resources;

namespace Application.Common.Exceptions;

public class CustomValidationException(IDictionary<string, string[]> errors)
    : AppException(Messages.Common.ValidationFailureMessage, 400)
{
    public IDictionary<string, string[]> Errors { get; } = errors;
}