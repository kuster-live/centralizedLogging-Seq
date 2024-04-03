namespace LoggingDemo.WebUi.Data;
using LoggingDemo.Shared;

public class WeatherForecastService
{
    private readonly IWeatherForecastApi _api;
    private readonly ILogger<WeatherForecastService> _logger;

    public WeatherForecastService(IWeatherForecastApi api,
                                  ILogger<WeatherForecastService> logger)
    {
        _api = api;
        _logger = logger;
    }

    public Task<WeatherForecast[]> GetForecastAsync(DateOnly startDate)
    {
        using var scope = _logger.BeginScope(new Dictionary<string, object> { ["StartDate"] = startDate });

        _logger.LogInformation("start api call");

        return _api.GetWeatherForecastsAsync(startDate.ToString("yyyy-MM-dd"));
    }
}
