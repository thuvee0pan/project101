using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using Catalog.API.Data;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;

// Add Services to the container.
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();
builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();
if (builder.Environment.IsDevelopment())
{
    builder.Services.InitializeMartenWith<CatalogInitialData>();
}
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.MapCarter();
app.UseExceptionHandler(options =>{});
app.UseHealthChecks("/health",
    new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

app.Run();