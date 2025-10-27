using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ordering.Infrastructure.Data;
using Ordering.Infrastructure.Data.Interceptors;

namespace Ordering.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices
            (this IServiceCollection services, IConfiguration configuration)
        {
            // Add infrastructure services here, e.g., DbContext, Repositories, External Services, etc.
            var connectionString = configuration.GetConnectionString("DataBase");

            services.AddDbContext<ApplicationDbContext>((sp,options) =>
            {

                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseSqlServer(connectionString);
            });

            return services;
        }
    }
}
