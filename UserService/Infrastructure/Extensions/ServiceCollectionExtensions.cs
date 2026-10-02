using Microsoft.Extensions.Options;
using Npgsql;
using UserService.Domain;
using UserService.Infrastructure.Configuration;
using UserService.Infrastructure.Persistence;
using UserService.Infrastructure.Persistence.Migrations;

namespace UserService.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDalHelpers(services, configuration);
        AddRepositories(services);

        return services;
    }

    private static void AddDalHelpers(IServiceCollection services, IConfiguration configuration)
    {
        services.AddOptions<PostgresOptions>()
            .Bind(configuration.GetRequiredSection(PostgresOptions.SectionName))
            .ValidateOnStart();

        services.AddSingleton(sp =>
        {
            var options = sp.GetRequiredService<IOptions<PostgresOptions>>().Value;
            return NpgsqlDataSource.Create(options.ConnectionString);
        });

        services.AddSingleton<DatabaseMigrationRunner>();
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddSingleton<UserDbModelMapper>();
        services.AddSingleton<IUserRepository, UserRepository>();
    }
}
