using System.Text.Json.Serialization;
using MediatR;
using profile_API.Context;
using profile_API.Hub;
using profile_API.Infastructure;
using profile_API.Infastructure.Configuration;
using profile_API.Middleware;
using profile_Application.Profile.User.CreateUser;
using profile_Core.Contracts;
using profile_Core.Model;
using profile_DataAccess;
using profile_DataAccess.Context;
using profile_Infastructure;
using profile_Service;
using Serilog;




// Создание билдера приложения
var builder = WebApplication.CreateBuilder(args);

// Настройка логирования
Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(builder.Configuration)
    .CreateLogger();

// Регистрация службы Serilog
builder.Host.UseSerilog();


// Миграция БД
builder.MigrateDatabase(new List<Action<ProfileDbContext, IServiceProvider, ILogger<ProfileDbContext>>>());

// Добавление сервисов в контейнер
ConfigureServices(builder.Services, builder.Configuration);

// Создание приложения
var app = builder.Build();

// Настройка HTTP request pipeline
ConfigureMiddleware(app);

app.Run();

// Метод для настройки сервисов
void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Получение параметров аутентификации
    var paramAuth = configuration.GetSection("Auth");

    // Настройка аутентификации
    services.Configure<Auth>(configuration.GetSection("Auth"));
    services.AddMediatR(typeof(CreateUserRequest).Assembly);
    services.AddControllers()
        .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
    
    // Регистрация других зависимостей
    services.AddServiceDependencies();
    services.AddDataAccessDependencies(configuration);
    services.AddHttpClient();
    services.AddHttpContextAccessor();
    services.AddScoped<IHttpContextService, HttpContextService>();
    services.AddAuth(configuration, paramAuth.Get<Auth>());
    services.AddSignalR();
}

// Метод для настройки промежуточного ПО
void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionHandlingMiddleware>();
    app.UseHttpsRedirection();
    app.UseRouting();

    app.UseAuthentication();
    app.UseAuthorization();

    app.UseEndpoints(endpoints =>
    {
        endpoints.MapHub<ChatHub>("/chathub");
        endpoints.MapControllers();
    });
}