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
        services.AddAutoMapper(_ => { }, Assembly.GetExecutingAssembly());
        services.AddMediatR(x => x.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<IRequestValidator, RequestValidator>();
        
        ValidatorOptions.Global.PropertyNameResolver = (_, memberInfo, _) =>
        {
            if (memberInfo == null) return null;

            var name = memberInfo.Name;
            return char.ToLowerInvariant(name[0]) + name.Substring(1);
        };

        return services;
    }
}