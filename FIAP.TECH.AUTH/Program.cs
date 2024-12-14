using FIAP.TECH.CORE.APPLICATION.Authentication;
using FIAP.TECH.CORE.APPLICATION.Configurations;
using FIAP.TECH.CORE.APPLICATION.Services.Users;
using FIAP.TECH.CORE.APPLICATION.Settings.JwtExtensions;
using FIAP.TECH.CORE.DOMAIN.Interfaces.Repositories;
using FIAP.TECH.INFRASTRUCTURE.Contexts;
using FIAP.TECH.INFRASTRUCTURE.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Add methods extensions
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Add DbContext
builder.Services.AddDbContextConfiguration(builder.Configuration);

builder.Services.AddSecurity();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "Docker")
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.MapPost("/login", [AllowAnonymous] async ([FromBody] AuthenticateRequest request, IUserService userService) =>
{
    var response = await userService.Authenticate(request);

    if (response is null)
        return Results.BadRequest(new { message = "Email e/ou senha inválido(s)" });

    return Results.Ok(response);
})
.WithName("login")
.WithOpenApi();

app.Run();

public partial class ProgramAuth { }