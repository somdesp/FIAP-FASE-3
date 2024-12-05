using FIAP.TECH.CORE.APPLICATION.Configurations;
using FIAP.TECH.WORK.Extensions;
using FIAP.TECH.WORK.Validation;
using FluentValidation;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMassTransitExtensionWork(context.Configuration);
        services.AddDbContextConfiguration(context.Configuration);
        services.AddValidatorsFromAssemblyContaining<ContactInsertValidation>();
    })
    .Build();



await host.RunAsync();