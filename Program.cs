
//Jonas da Rosa Oliveira
//https://www.linkedin.com/in/jonas-da-rosa-oliveira

using RabbitWorkerApi.Infrastructure.Messaging;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSingleton<RabbitMqProducer>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); 
    app.MapScalarApiReference(options => 
    {
        options.WithOpenApiRoutePattern("/openapi/v1.json");
    });
}

app.UseHttpsRedirection();

app.Run();