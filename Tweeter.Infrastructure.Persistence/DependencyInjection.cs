using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Domain.Contracts.Persistence;
using Tweeter.Infrastructure.Persistence._Data;

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

			return services;
		}
	}
}
