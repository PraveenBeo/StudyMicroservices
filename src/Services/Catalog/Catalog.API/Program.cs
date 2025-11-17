using Carter;
using Catalog.API.Behaviors;
using Catalog.API.Exceptions.Handler;
using FluentValidation;
using FluentValidation.Validators;
using Marten;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
    cfg.AddOpenBehavior(typeof(ValidationBehavior<,>));
    cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
builder.Services.AddCarter();
builder.Services.AddMarten(options =>
{
    options.Connection(builder.Configuration.GetConnectionString("MartenConnection") ?? string.Empty);
}).UseLightweightSessions();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("MartenConnection") ?? string.Empty);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter=UIResponseWriter.WriteHealthCheckUIResponse
    });
app.Run();
