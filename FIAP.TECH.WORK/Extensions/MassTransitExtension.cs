using FIAP.TECH.WORK.Events;
using MassTransit;
using System.Text.Json.Serialization;

namespace FIAP.TECH.WORK.Extensions
{
    public static class MassTransitExtension
    {
        public static void AddMassTransitExtensionWork(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddMassTransit(opt =>
            {
                opt.AddConsumer<QueueCreateUpdateContactConsumer>();
                opt.AddConsumer<QueueErrosContactConsumer>();
                opt.AddConsumer<QueueConsultContactConsumer>();

                opt.SetKebabCaseEndpointNameFormatter();

                opt.UsingRabbitMq(
                    (context, cfg) =>
                    {
                        cfg.ConfigureJsonSerializerOptions(json =>
                        {
                            json.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                            json.WriteIndented = true;
                            return json;
                        });

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
