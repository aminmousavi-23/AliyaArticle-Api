using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using WebAPI.Middlewares;

namespace WebAPI;

public static class StartupHelperExtensions
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        var uiDomains = configuration.GetSection("UIDomains").Get<string[]>();

        services.AddOpenApi();
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddHealthChecks();
        services.AddCors(options =>
        {
            options.AddPolicy("AllowUI", builder =>
                builder
                    .WithOrigins(uiDomains ?? throw new InvalidOperationException("Null UI Domain"))
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials());
        });

        return services;
    }

    public static WebApplication AddMiddlewares(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(options =>
            {
                options.WithTitle("AliyaNews.Api");
                options.WithTheme(ScalarTheme.BluePlanet);
            });
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseCors("AllowUI");
        app.UseAuthorization();
        app.MapControllers();
        app.MapHealthChecks("/health");
        return app;
    }

    public static void AddConfiguration(this IConfigurationBuilder configurationBuilder, IHostEnvironment environment)
    {
        configurationBuilder
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
    }

    public static async Task ResetDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        
        var context = scope.ServiceProvider.GetService<AppDbContext>();
        if (context != null)
        {
            await context.Database.EnsureDeletedAsync();
            await context.Database.MigrateAsync();
        }
    }
}