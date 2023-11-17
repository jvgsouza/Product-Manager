using Usuario.Domain.Entities;
using Usuario.Domain.Services;

namespace Usuario.Application.Services
{
    public class WeatherForecastService : IWeatherForecastService
    {
        public List<WeatherForecast> GetWeatherForecast()
        {
            var list = new List<WeatherForecast>();
            for (int i = 0; i < 5; i++)
            {
                var weatherForecast = new WeatherForecast();
                weatherForecast.TemperatureF = Random.Shared.Next(-20, 55);
                list.Add(weatherForecast);
            }
            return list;
        }
    }
}
