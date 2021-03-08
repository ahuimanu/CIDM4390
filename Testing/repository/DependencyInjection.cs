using domain;
using domain.NOAAStationAggregate;
using domain.VatsimMETARAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace repository
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddTransient<INOAAStationRepository, NOAAStationRepository>();
            services.AddTransient<IVatsimMETARRepository, VatsimMETARRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();

            services.AddDbContext<WebApiDbContext>(options => options.UseInMemoryDatabase(databaseName: "Weather"));            
            

            return services;
        }
    }
}