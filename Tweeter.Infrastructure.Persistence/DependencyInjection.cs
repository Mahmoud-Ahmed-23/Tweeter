using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tweeter.Core.Domain.Contracts.Persistence;
using Tweeter.Infrastructure.Persistence._Data;
using Tweeter.Infrastructure.Persistence.Repositories.Generic_Repository;

namespace Tweeter.Infrastructure.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<TweeterDbContext>((provider, options) =>
            {
                var connectionString = configuration.GetConnectionString("TweeterContext");
                options.UseSqlServer(connectionString);
            });

            services.AddScoped(typeof(IDbInitializer), typeof(TweeterDbInitializer));
            services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            return services;
        }
    }
}
