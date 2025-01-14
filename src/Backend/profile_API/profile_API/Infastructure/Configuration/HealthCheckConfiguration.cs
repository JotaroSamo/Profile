using Microsoft.Extensions.Diagnostics.HealthChecks;
using MongoDB.Driver;

namespace profile_API.Infastructure.Configuration;

public static class HealthCheckConfiguration
{
    public static void AddApplicationHealthChecks(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddHealthChecks()
            .AddCheck("Application API",
                _ => HealthCheckResult.Healthy(),
                tags: new[] { "app_api", "api" })
            .AddMongoDb(
                clientFactory: sp => new MongoClient(configuration.GetConnectionString("MongoDb")),
                databaseNameFactory: sp => "logsDatabase", 
                name: "Logs Storage",
                tags: new[] { "mongodb", "logs" })
            .AddNpgSql(
                configuration.GetConnectionString("Profiles"),
                name: "Data Storage",
                tags: new[] { "postgresql", "data" });
    }

}