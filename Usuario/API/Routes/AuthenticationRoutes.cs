using Usuario.Domain.Services;

namespace Usuario.API.Routes
{
    public static class AuthenticationRoutes
    {
        public static void AuthenticationEndpoints(this WebApplication app)
        {
            var group = app.MapGroup("api/v1/Authentication")
                .WithTags("Authentication")
                .WithOpenApi();

            group.MapGet("/Login", Login)
            .Produces(404)
            .Produces<int>(200)
            .WithName("GetWeatherForecast");
        }

        private static IResult Login(IUserService service)
        {
            var user = service.Login();
            return Results.Ok(user);
        }
    }
}
