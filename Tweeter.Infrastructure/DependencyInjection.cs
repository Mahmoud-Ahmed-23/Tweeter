using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;
using Tweeter.Core.Domain.Contracts.Infrastructure;
using Tweeter.Infrastructure.AttachementServices;
using Tweeter.Infrastructure.CacheServices;

namespace Tweeter.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient(typeof(IAttachmentService), typeof(AttachmentService));

			services.AddSingleton(typeof(ICacheService), typeof(CacheService));

			services.AddSingleton(typeof(IConnectionMultiplexer), (serviceProvider) =>
			{
				var connectionString = configuration.GetConnectionString("Redis");

				var connectionMultiplexer = ConnectionMultiplexer.Connect(connectionString!);

				return connectionMultiplexer;
			});
			
			return services;
		}
	}
}
