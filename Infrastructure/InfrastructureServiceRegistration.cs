using System.Text;
using Application.Abstractions.Infrastructure;
using Application.Abstractions.Persistence;
using Infrastructure.Common.Constants;
using Infrastructure.Common.Options;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure;

public static class InfrastructureServiceRegistration
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("SqliteConnectionString"));
        });
        services.AddJwtAuthentication(configuration);
        services.AddSingleton<IJwtTokenService, JwtTokenService>();
        
        services.AddHttpContextAccessor();
        services.AddScoped<IUserContextAccessor, UserContextAccessor>();

        #region Repositories

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IArticleRepository, ArticleRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped<ICommentRepository, CommentRepository>();
        services.AddScoped<ITagRepository, TagRepository>();

        #endregion
        
        return services;
    }
    
    private static IServiceCollection AddJwtAuthentication(this IServiceCollection services,
        IConfiguration configuration)
    {
        var jwtConfigOptions = configuration.GetSection("JwtConfigOptions").Get<JwtConfigOptions>();
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfigOptions!.Issuer,
                    ValidAudience = jwtConfigOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfigOptions.Key))
                };
                options.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        Console.WriteLine("Authentication failed: " + context.Exception.Message);
                        Console.WriteLine("Token: " + context.Request.Headers.Authorization);
                        return Task.CompletedTask;
                    }
                };
            });
        services.Configure<JwtConfigOptions>(configuration.GetSection("JwtConfigOptions"));
        
        services.AddAuthorization();

        return services;
    }
}