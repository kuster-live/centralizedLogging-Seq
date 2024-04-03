namespace LoggingDemo.Shared;

using Refit;

public interface IWeatherForecastApi
{
    [Get("/weatherforecast/{dateOnly}")]
    Task<WeatherForecast[]> GetWeatherForecastsAsync([Query] string dateOnly);
}
