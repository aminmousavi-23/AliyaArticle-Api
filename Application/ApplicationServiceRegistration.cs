using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        
        services.AddAutoMapper(_ => { }, assembly);
        services.AddMediatR(x => x.RegisterServicesFromAssembly(assembly));
        services.AddValidatorsFromAssembly(assembly);
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