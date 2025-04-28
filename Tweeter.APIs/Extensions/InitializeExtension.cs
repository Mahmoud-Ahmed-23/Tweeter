using Tweeter.Core.Domain.Contracts.Persistence;

namespace Tweeter.APIs.Extensions
{
	public static class InitializeExtension
	{
		public static async Task<WebApplication> InitializeDatabaseAsync(this WebApplication app)
		{
			using (var scope = app.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				var LoggerFactory = services.GetRequiredService<ILoggerFactory>();

				try
				{
					var dbInitializer = services.GetRequiredService<IDbInitializer>();
					await dbInitializer.InitializeAsync();
				}
				catch (Exception ex)
				{
					var Logger = LoggerFactory.CreateLogger<Program>();
					Logger.LogError(ex, "an error has been occured during applaying migrations");
				}

				return app;
			}
		}
	}
}
