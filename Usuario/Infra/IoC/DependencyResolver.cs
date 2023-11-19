using Usuario.Application.Services;
using Usuario.Domain.Repositories;
using Usuario.Domain.Services;
using Usuario.Infra.Data;
using Usuario.Infra.Repositories;

namespace Usuario.Infra.IoC
{
    public static class DependencyResolver
    {
        public static void AddDependencyResolver(this IServiceCollection services)
        {
            RegisterServices(services);
            RegisterRepositories(services);
            RegisterDb(services);
        }

        private static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
        }

        private static void RegisterRepositories(IServiceCollection services)
        {
            services.AddScoped<IUserRepository, UserRepository>();
        }

        private static void RegisterDb(IServiceCollection services)
        {
            services.AddScoped<IDbConnection, DatabaseConnection>();
        }
    }
}
