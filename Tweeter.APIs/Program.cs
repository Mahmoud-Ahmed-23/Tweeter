
using System.Threading.Tasks;
using Tweeter.APIs.Extensions;
using Tweeter.APIs.Middlewares;
using Tweeter.Core.Application;
using Tweeter.Infrastructure.Persistence;

namespace Tweeter.APIs
{
	public class Program
	{
		public static async Task Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			builder.Services.AddControllers();
			// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
			builder.Services.AddOpenApi();

			builder.Services.AddPersistence(builder.Configuration);
			builder.Services.AddApplicationServices(builder.Configuration);
			builder.Services.RegesteredPresestantLayer();
			builder.Services.AddIdentityServices(builder.Configuration);



			var app = builder.Build();

			await app.InitializeDatabaseAsync();

			app.UseMiddleware<ExeptionHandlerMiddleware>();

			app.UseRouting();
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.MapOpenApi();
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseStatusCodePagesWithReExecute("/Errors/{0}");

			app.UseAuthentication();
			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
