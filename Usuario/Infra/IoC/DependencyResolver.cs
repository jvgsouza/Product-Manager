using Usuario.Application.Services;
using Usuario.Domain.Services;

namespace Usuario.Infra.IoC
{
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            RegisterServices(services);
            RegisterRepositories(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
        }
    }
}
