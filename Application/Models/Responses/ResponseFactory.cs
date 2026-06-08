using Microsoft.AspNetCore.Http;

namespace Application.Models.Responses;

public static class ResponseFactory
{
    public static BaseResponse Ok(string? message = null) =>
        new()
        {
            StatusCode = StatusCodes.Status200OK,
            Message = message
        };

    public static BaseResponse<T> Ok<T>(T data, string? message = null) =>
        new()
        {
            StatusCode = StatusCodes.Status200OK,
            Message = message,
            Data = data
        };

    public static CollectionResponse<T> Ok<T>(IEnumerable<T> data, long totalCount, string? message = null) =>
        new()
        {
            StatusCode = StatusCodes.Status200OK,
            Message = message,
            Data = data,
            TotalCount = totalCount
        };

    public static BaseResponse Created(string? message = null) =>
        new()
        {
            StatusCode = StatusCodes.Status201Created,
            Message = message
        };

    public static BaseResponse<T> Created<T>(T data, string? message = null) =>
        new()
        {
            StatusCode = StatusCodes.Status201Created,
            Message = message,
            Data = data
        };

    public static BaseResponse<T> BadRequest<T>(string message) =>
        new()
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Message = message
        };

    public static BaseResponse ValidationError(List<ValidationErrorResponse> errors, string? message = null) =>
        new()
        {
            StatusCode = StatusCodes.Status400BadRequest,
            Message = message,
            ValidationErrors = errors
        };

    public static BaseResponse<T> NotFound<T>(string message) =>
        new()
        {
            StatusCode = StatusCodes.Status203NonAuthoritative,
            Message = message
        };
    
    public static CollectionResponse<T> CollectionNotFound<T>(string message) =>
        new()
        {
            StatusCode = StatusCodes.Status203NonAuthoritative,
            Message = message
        };

    public static BaseResponse Unauthorized(string? message = null) =>
        new()
        {
            StatusCode = StatusCodes.Status401Unauthorized,
            Message = message
        };

    public static BaseResponse Forbidden(string? message = null) =>
        new()
        {
            StatusCode = StatusCodes.Status403Forbidden,
            Message = message
        };

    public static BaseResponse Conflict(string message) =>
        new()
        {
            StatusCode = StatusCodes.Status409Conflict,
            Message = message
        };
    
    public static BaseResponse<T> Conflict<T>(string message) =>
        new()
        {
            StatusCode = StatusCodes.Status409Conflict,
            Message = message
        };

    public static BaseResponse InternalServerError(string? message = null) =>
        new()
        {
            StatusCode = StatusCodes.Status500InternalServerError,
            Message = message
        };
}