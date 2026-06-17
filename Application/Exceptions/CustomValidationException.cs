using Application.Common.Resources;
using Microsoft.AspNetCore.Http;

namespace Application.Exceptions;

public class CustomValidationException(IDictionary<string, string[]> errors) 
    : AppException(Messages.Common.BadRequest, StatusCodes.Status400BadRequest)
{
    public IDictionary<string, string[]> Errors { get; } = errors;
}