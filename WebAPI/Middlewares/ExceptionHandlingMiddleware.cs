using Application.Common.Resources;
using Application.Exceptions;
using Application.Models.Responses;
using Newtonsoft.Json;

namespace WebAPI.Middlewares;

public class ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (CustomValidationException ex)
        {
            var errors = ex.Errors?.Select(err => new ValidationErrorResponse()
            {
                Key = err.Key,
                Value = err.Value
            }).ToList();

            var response = ResponseFactory.ValidationError(errors!, ex.Message);

            await WriteJsonResponseAsync(context, response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Messages.Common.InternalServerError);

            var response = ResponseFactory.InternalServerError(ex.Message);

            await WriteJsonResponseAsync(context, response);
        }
    }

    private static async Task WriteJsonResponseAsync(HttpContext context, BaseResponse response)
    {
        context.Response.StatusCode = response.StatusCode;
        context.Response.ContentType = "application/json";

        var jsonResponse = JsonConvert.SerializeObject(response);
        await context.Response.WriteAsync(jsonResponse);
    }
}