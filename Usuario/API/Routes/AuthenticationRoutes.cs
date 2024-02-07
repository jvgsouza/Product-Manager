using Microsoft.AspNetCore.Mvc;
using Usuario.Domain.DTOs;
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

            group.MapPost("/Login", Login)
            .Produces(404)
            .Produces<int>(200);
        }

        public static IResult Login(IUserService service, [FromBody] Login login)
        {
            var user = service.Login(login);
            return Results.Ok(user);
        }
    }
}
