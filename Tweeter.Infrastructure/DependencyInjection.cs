using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tweeter.Core.Domain.Contracts.Infrastructure;
using Tweeter.Infrastructure.AttachementServices;

namespace Tweeter.Infrastructure
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddTransient(typeof(IAttachmentService), typeof(AttachmentService));

			return services;
		}
	}
}
