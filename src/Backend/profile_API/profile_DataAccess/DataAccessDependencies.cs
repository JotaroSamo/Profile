using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using profile_DataAccess.Context;
using profile_MapperModel.Mapper;

namespace profile_DataAccess;

public static class DataAccessDependencies
{
    public static IServiceCollection AddDataAccessDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProfileDbContext>(options => options.UseNpgsql(configuration.GetConnectionString("Profiles")));
        services.AddAutoMapper(typeof(UserProfile).Assembly);
        return services;
    }
    
}