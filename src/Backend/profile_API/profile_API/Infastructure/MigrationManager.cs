using Microsoft.EntityFrameworkCore;

namespace profile_API.Infastructure;

public static class MigrationManager
{
    public static WebApplication MigrateDatabase<TContext>(this WebApplication builder, List<Action<TContext, IServiceProvider, ILogger<TContext>>> seeders)
        where TContext : DbContext
    {
        using var scope = builder.Services.CreateScope();

        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<TContext>();

        var logger = services.GetRequiredService<ILogger<TContext>>();
        var contextName = typeof(TContext).Name;

        try
        {
            logger.LogInformation("Migrating database associated with context {ContextName}", contextName);

            context.Database.Migrate();
            foreach (var seeder in seeders)
            {
                seeder(context, services, logger);
            }

            logger.LogInformation("Database associated with context {ContextName} has been migrated", contextName);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occurred while migrating the database used on context {ContextName}, {se}", contextName, services);
            Console.WriteLine(ex);
        }
        finally
        {
            context.Dispose();
        }

        return builder;
    }
}
