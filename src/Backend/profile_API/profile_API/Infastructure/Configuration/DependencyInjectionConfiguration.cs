using profile_API.Context;
using profile_API.HostedService;
using profile_API.Hub;
using profile_Core.Contracts;

namespace profile_API.Infastructure.Configuration;

public static class DependencyInjectionConfiguration
{
    public static void AddDependencyInjection(this IServiceCollection services)
    {
        services.AddScoped<IHttpContextService, HttpContextService>();
        services.AddSingleton<ChatHub>();
        services.AddHostedService<MessageCheckerService>();
    }
}