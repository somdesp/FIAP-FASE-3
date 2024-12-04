using MassTransit;

namespace FIAP.TECH.API.CREATION.DependencyInjection
{
    public static class MassTransitExtension
    {
        public static void AddMassTransitExtensionWeb(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMassTransit(opt =>
            {
                opt.SetKebabCaseEndpointNameFormatter();

                opt.UsingRabbitMq(
                    (context, cfg) =>
                    {
                        cfg.Host(configuration.GetConnectionString("RabbitMq"));
                    });

            });
        }
    }
}
