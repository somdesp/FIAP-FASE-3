using FIAP.TECH.CORE.APPLICATION.Configurations;
using FIAP.TECH.WORK.Extensions;
using FIAP.TECH.WORK.Validation;
using FluentValidation;
using TinyHealthCheck;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMassTransitExtensionWork(context.Configuration);
        services.AddDbContextConfiguration(context.Configuration);
        services.AddValidatorsFromAssemblyContaining<ContactInsertValidation>();
        services.AddBasicTinyHealthCheckWithUptime(c =>
                    {
                        c.Port = 5001;
                        c.Hostname = "*";
                        c.UrlPath = "/healthcheck";
                        return c;
                    });
    })
    .Build();



await host.RunAsync();