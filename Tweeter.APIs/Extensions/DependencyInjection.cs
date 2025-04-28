using Microsoft.AspNetCore.Mvc;
using Tweeter.Apis.Controllers;
using Tweeter.APIs.Services;
using Tweeter.Core.Application.Abstraction;
using Tweeter.Shared.ErrorModule.Errors;

namespace Tweeter.APIs.Extensions
{
	public static class DependencyInjection
	{
		public static IServiceCollection RegesteredPresestantLayer(this IServiceCollection services)
		{
			services.AddScoped(typeof(ILoggedInUserService), typeof(LoggedInUserService));


			#region Swagger
			services.AddEndpointsApiExplorer();
			services.AddSwaggerGen();

			#endregion

			services.AddHttpContextAccessor();


			services.AddControllers().ConfigureApiBehaviorOptions(options =>
			{
				options.SuppressModelStateInvalidFilter = false;
				options.InvalidModelStateResponseFactory = (actionContext =>
				{
					var Errors = actionContext.ModelState.Where(e => e.Value!.Errors.Count() > 0)
												.SelectMany(e => e.Value!.Errors).Select(e => e.ErrorMessage);

					return new BadRequestObjectResult(new ApiValidationErrorResponse() { Errors = Errors });

				});
			}
			).AddApplicationPart(typeof(AssemblyInformation).Assembly);

			return services;
		}
	}
}
