using System.Diagnostics.CodeAnalysis;
using Usuario.Application.Services;
using Usuario.Domain.Services;

namespace Usuario.Application.IoC
{
    [ExcludeFromCodeCoverage]
    public static class ServiceDependencyResolver
    {
        public static void AddServiceDependencyResolver(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
