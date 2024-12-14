using FIAP.TECH.CORE.APPLICATION.Configurations;
using FIAP.TECH.CORE.APPLICATION.Settings.JwtExtensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddRouting(options => options.LowercaseUrls = true);

// Add Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerConfiguration();

// Add methods extensions
builder.Services.AddInjectionApplication(builder.Configuration);

builder.Services.Configure<TokenSettings>(builder.Configuration.GetSection("TokenSettings"));
builder.Services.AddSecurity();

builder.Services.AddMassTransitExtensionWeb(builder.Configuration);
// Add DbContext
builder.Services.AddDbContextConfiguration(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseRouting();

//app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

//app.UseHttpMetrics();

app.Run();

public partial class ProgramCreation { }