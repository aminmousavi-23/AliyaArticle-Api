using System.Reflection;
using Application.Common.Validation;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
        services.AddMediatR(c => c.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddScoped<IRequestValidator, RequestValidator>();
        
        ValidatorOptions.Global.PropertyNameResolver = (type, memberInfo, expression) =>
        {
            if (memberInfo == null) return null;

            var name = memberInfo.Name;
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        };

        return services;
    }
}