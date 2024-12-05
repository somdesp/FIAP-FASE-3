using FIAP.TECH.CORE.APPLICATION.Services.Contacts;
using FIAP.TECH.CORE.APPLICATION.Services.Users;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.INFRASTRUCTURE.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Text.Json.Serialization;

namespace FIAP.TECH.CORE.APPLICATION.Configurations;

public static class ConfigureServices
{
    public static void AddInjectionApplication(this IServiceCollection services, IConfiguration Configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        // Repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IContactRepository, ContactRepository>();
        services.AddScoped<IRegionRepository, RegionRepository>();

        // Services
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IContactService, ContactService>();


        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            options.JsonSerializerOptions.WriteIndented = true;
        });
    }
}
