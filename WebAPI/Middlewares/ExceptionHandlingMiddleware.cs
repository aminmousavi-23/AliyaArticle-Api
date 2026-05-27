using Application.Common.Exceptions;
using Application.Common.Resources;
using Application.Common.Responses;

namespace WebAPI.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (CustomValidationException ex)
        {
            var response = new BaseResponse
            {
                StatusCode = ex.StatusCode,
                Message = ex.Message,
                ValidationErrors = ex.Errors?.Select(err => new ValidationErrorResponse()
                {
                    Key = err.Key,
                    Value = err.Value
                }).ToList()
            };

            await WriteJsonResponseAsync(context, response, ex.StatusCode);
        }
        catch (AppException ex)
        {
            _logger.LogError(ex, ex.Message);

            var response = new BaseResponse
            {
                StatusCode = ex.StatusCode,
                Message = ex.Message,
            };

            await WriteJsonResponseAsync(context, response, ex.StatusCode);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, Messages.Common.InternalServerError);

            var response = new BaseResponse
            {
                StatusCode = StatusCodes.Status500InternalServerError,
                Message = Messages.Common.InternalServerError
            };

            await WriteJsonResponseAsync(context, response, StatusCodes.Status500InternalServerError);
        }
    }

    private static async Task WriteJsonResponseAsync(HttpContext context, BaseResponse response, int statusCode)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        var jsonResponse = JsonConvert.SerializeObject(response);
        await context.Response.WriteAsync(jsonResponse);
    }
}