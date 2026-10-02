using System.Reflection;
using DbUp;
using Microsoft.Extensions.Options;
using UserService.Infrastructure.Configuration;

namespace UserService.Infrastructure.Persistence.Migrations;

public sealed class DatabaseMigrationRunner(IOptions<PostgresOptions> options)
{
    public void Run()
    {
        var upgradeEngine = DeployChanges.To
            .PostgresqlDatabase(options.Value.ConnectionString)
            .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
            .Build();

        var result = upgradeEngine.PerformUpgrade();

        if (!result.Successful)
        {
            throw new InvalidOperationException("Failed to apply DB migration", result.Error);
        }
    }
}
