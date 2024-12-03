using FIAP.TECH.INFRASTRUCTURE.DependencyInjection;
using FIAP.TECH.WORK.Extensions;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddMassTransitExtensionWork(builder.Configuration);
builder.Services.ConfigureDbContextExtension(builder.Configuration);



var host = builder.Build();
host.Run();
