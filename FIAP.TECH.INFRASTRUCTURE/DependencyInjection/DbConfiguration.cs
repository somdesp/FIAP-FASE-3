using FIAP.TECH.INFRASTRUCTURE.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FIAP.TECH.INFRASTRUCTURE.DependencyInjection;

public static class DbConfiguration
{
    public static void ConfigureDbContextExtension(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("FiapConnection");
        services.AddDbContext<AppDbContext>((opt) => opt.UseSqlServer(connection), ServiceLifetime.Scoped);
    }
}
