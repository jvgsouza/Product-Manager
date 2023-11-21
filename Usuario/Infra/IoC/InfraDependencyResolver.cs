using Usuario.Application.Services;
using Usuario.Domain.Repositories;
using Usuario.Domain.Services;
using Usuario.Infra.Data;
using Usuario.Infra.Repositories;

namespace Usuario.Infra.IoC
{
    public static class InfraDependencyResolver
    {
        public static void AddInfraDependencyResolver(this IServiceCollection services)
        {
            RegisterRepositories(services);
            RegisterDb(services);
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
