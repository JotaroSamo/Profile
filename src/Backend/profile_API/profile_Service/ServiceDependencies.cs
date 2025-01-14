using Microsoft.Extensions.DependencyInjection;
using profile_Core.Chat;
using profile_Core.JWT;
using profile_Core.Password;
using profile_Core.Profile;
using profile_Service.Chat;
using profile_Service.JWT;
using profile_Service.Password;
using profile_Service.Profile;

namespace profile_Service;

public static class ServiceDependencies
{
    public static IServiceCollection AddServiceDependencies(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IPostService, PostService>();
        services.AddScoped<IPasswordService, PasswordService>();
        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IChatService,ChatService>();
        services.AddScoped<IMessageService, MessageService>();
        services.AddScoped<IUserChatConnectionService, UserChatConnectionService>();
        return services;
    }
}