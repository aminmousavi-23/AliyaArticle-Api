using Application;
using Infrastructure;
using WebAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddApplicationServices()
    .AddInfrastructureServices(builder.Configuration)
    .AddApiServices(builder.Configuration);

builder.Configuration
    .AddConfiguration(builder.Environment);

var app = builder.Build();

app.AddMiddlewares();

await app.MigrateDatabaseAsync();

app.Run();