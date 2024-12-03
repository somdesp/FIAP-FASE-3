using FIAP.TECH.WORK.Events;
using MassTransit;

namespace FIAP.TECH.WORK.Extensions
{
    public static class MassTransitExtension
    {
        public static void AddMassTransitExtensionWork(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMassTransit(opt =>
            {
                opt.AddConsumer<QueueCreateContactConsumer>(typeof(QueueContactConsumerDefinition));

                opt.SetKebabCaseEndpointNameFormatter();

                opt.UsingRabbitMq(
                    (context, cfg) =>
                    {
                        cfg.Host(configuration.GetConnectionString("RabbitMq"));

                        cfg.ServiceInstance(instance =>
                        {
                            instance.ConfigureJobServiceEndpoints();
                            instance.ConfigureEndpoints(context, new KebabCaseEndpointNameFormatter("fiap", false));

                        });
                    });

            });
        }
    }

}
