using FIAP.TECH.INFRASTRUCTURE.DependencyInjection;
using FIAP.TECH.WORK.Extensions;
using FIAP.TECH.WORK.Validation;
using FluentValidation;

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((context, services) =>
    {
        services.AddMassTransitExtensionWork(context.Configuration);
        services.ConfigureDbContextExtension(context.Configuration);
        services.AddValidatorsFromAssemblyContaining<ContactInsertValidation>();
    })
    .Build();

await host.RunAsync();