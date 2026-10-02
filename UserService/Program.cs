using UserService.Application.Extensions;
using UserService.Controllers;
using UserService.Infrastructure.Extensions;
using UserService.Infrastructure.Persistence.Migrations;
using UserService.Mapping.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddGrpc();

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddMapping();
builder.Services.AddApplication();

var app = builder.Build();

app.Services.GetRequiredService<DatabaseMigrationRunner>().Run();

app.MapGrpcService<UserServiceController>();

await app.RunAsync();
