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
        catch (AppException ex)
        {
            logger.LogError(ex, ex.Message);
            
            var response = ResponseFactory.Exception(ex.Message, ex.StatusCode);

            await WriteJsonResponseAsync(context, response);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, Messages.Common.InternalServerError);

            var response = ResponseFactory.Exception(ex.Message);

            await WriteJsonResponseAsync(context, response);
        }
    }

    private static async Task WriteJsonResponseAsync(HttpContext context, BaseResponse response)
    {
        context.Response.StatusCode = response.StatusCode;
        context.Response.ContentType = "application/json";
        
        var settings = new JsonSerializerSettings
        {
            ContractResolver = new Newtonsoft.Json.Serialization.CamelCasePropertyNamesContractResolver()
        };

        var jsonResponse = JsonConvert.SerializeObject(response, settings);
        await context.Response.WriteAsync(jsonResponse);
    }
}