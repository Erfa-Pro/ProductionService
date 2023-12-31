using Erfa.ProductionManagement.Persistence.Repositories;
using Erfa.ProductionManagement.Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Erfa.ProductionManagement.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ErfaDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("PostgreSQLConnString")));
            services.AddScoped(typeof(IAsyncRepository<>), typeof(BaseRepository<>));

            services.AddScoped<ICatalogRepository, CatalogRepository>();

            return services;
        }
    }
}
