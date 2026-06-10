using Microsoft.AspNetCore.Http;

namespace Application.Exceptions;

public class AppException(string message, int statusCode = StatusCodes.Status500InternalServerError)
    : Exception(message)
{
    public int StatusCode { get; } = statusCode;
}