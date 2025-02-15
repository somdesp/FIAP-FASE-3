using FIAP.TECH.CORE.APPLICATION.Configurations;
using FIAP.TECH.WORK.Extensions;
using FIAP.TECH.WORK.Validation;
using FluentValidation;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks();
builder.Services.AddMassTransitExtensionWork(builder.Configuration);
builder.Services.AddDbContextConfiguration(builder.Configuration);
builder.Services.AddValidatorsFromAssemblyContaining<ContactInsertValidation>();
builder.Services.AddHealthChecks();
var app = builder.Build();

app.MapHealthChecks("/healthz");

app.Run();