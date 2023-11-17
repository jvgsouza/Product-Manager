using Usuario.Domain.Entities;

namespace Usuario.Domain.Services
{
    public interface IWeatherForecastService
    {
        List<WeatherForecast> GetWeatherForecast();
    }
}
