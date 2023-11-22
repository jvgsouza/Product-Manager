using Usuario.Application.Services;
using Usuario.Domain.Services;

namespace Usuario.Application.IoC
{
    public static class ServiceDependencyResolver
    {
        public static void AddServiceDependencyResolver(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }
    }
}
