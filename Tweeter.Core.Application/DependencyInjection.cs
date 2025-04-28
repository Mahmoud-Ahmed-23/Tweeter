using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Services.Identity.Account;
using Tweeter.Core.Application.Abstraction.Services.Identity.Authentication;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Services.Identity.Account;
using Tweeter.Core.Application.Services.Identity.Authentication;
using Tweeter.Shared.Settings;

namespace Tweeter.Core.Application
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));

			services.AddAutoMapper(Assembly.GetExecutingAssembly());

			services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

			var JwtSettings = new JwtSettings();

			configuration.GetSection(nameof(JwtSettings)).Bind(JwtSettings);

			services.AddSingleton(JwtSettings);

			services.AddScoped<IBaseHandler, BaseHandler>();

			services.AddScoped(typeof(IAccountService), typeof(AccountService));

			services.AddScoped(typeof(IAuthenticationService), typeof(AuthenticationService));


			return services;
		}
	}
}
