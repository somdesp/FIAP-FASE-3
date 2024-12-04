using FIAP.TECH.API.CREATION.DependencyInjection;
using FIAP.TECH.API.CREATION.Services;
using FIAP.TECH.INFRASTRUCTURE.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransitExtensionWeb(builder.Configuration);
// Add DbContext
builder.Services.ConfigureDbContextExtension(builder.Configuration);

builder.Services.AddScoped<ICreationService, CreationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
