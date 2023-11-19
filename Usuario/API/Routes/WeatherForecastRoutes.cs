using Microsoft.AspNetCore.Mvc;
using Usuario.Domain.Services;

namespace Usuario.API.Routes
{
    public static class WeatherForecastRoutes
    {
        public static void WeatherForecastEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/v1/WeatherForecast")
                .WithTags("WeatherForecast")
                .WithOpenApi();

            group.MapGet("/", GetWeatherForecast)
            .Produces(404)
            .Produces<int>(200)
            .WithName("GetWeatherForecast");

            group.MapGet("/TestLog", TestLogGet)
            .Produces(404)
            .Produces<int>(200);

            group.MapPost("/TestLog", TestLogPost)
            .Produces(404)
            .Produces<int>(200);
        }

        private static IResult GetWeatherForecast(IWeatherForecastService service)
        {
            var weatherForecast = service.GetWeatherForecast();
            return Results.Ok(weatherForecast);
        }

        private static IResult TestLogGet([FromQuery] string id)
        {
            return Results.Ok(id);
        }

        private static IResult TestLogPost([FromBody] string id)
        {
            return Results.Ok(id);
        }
    }
}
