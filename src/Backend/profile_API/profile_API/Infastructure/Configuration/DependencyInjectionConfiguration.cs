using profile_API.Context;
using profile_Core.Contracts;

namespace profile_API.Infastructure.Configuration;

public static class DependencyInjectionConfiguration
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IHttpContextService, HttpContextService>();
        
    }
}