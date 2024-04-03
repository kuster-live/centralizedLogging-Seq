using LoggingDemo.Api;
using LoggingDemo.Shared;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Events;
using static Microsoft.AspNetCore.Http.Results;
using ILogger = Microsoft.Extensions.Logging.ILogger;

var builder = WebApplication.CreateBuilder(args);



builder.Host
       .AddSerilog();


// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast/{dateOnly}", (string dateOnly, [FromServices] ILogger<Program> logger) =>
{
    if (!DateOnly.TryParse(dateOnly, out var date))
    {
        // logger.LogError("invalid date format {Date}", dateOnly);
        logger.ErrorInvalidDate(dateOnly);
        return BadRequest("Invalid date format, expected format is yyyy-MM-dd");
    }

    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            date.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return TypedResults.Ok(forecast);
})
.WithName("GetWeatherForecast")
.WithOpenApi();

app.Run();

