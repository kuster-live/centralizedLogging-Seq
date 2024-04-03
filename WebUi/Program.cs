using LoggingDemo.Shared;
using LoggingDemo.WebUi.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Refit;

var builder = WebApplication.CreateBuilder(args);

builder.Host
       .AddSerilog();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<WeatherForecastService>();

builder.Services.AddRefitClient<IWeatherForecastApi>()
       .ConfigureHttpClient(c =>
           c.BaseAddress =
               new(builder.Configuration.GetConnectionString("Api") ?? throw new InvalidOperationException()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();
