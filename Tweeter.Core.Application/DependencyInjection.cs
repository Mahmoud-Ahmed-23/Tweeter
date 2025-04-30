using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tweeter.Core.Application.Abstraction.Services.Emails;
using Tweeter.Core.Application.Abstraction.Services.Identity.Account;
using Tweeter.Core.Application.Abstraction.Services.Identity.Authentication;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Identity.Account.Command.Validators;
using Tweeter.Core.Application.Features.Identity.Authentication.Command.Validators;
using Tweeter.Core.Application.Features.Identity.Behaviors;
using Tweeter.Core.Application.Services.Emails;
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

            services.AddValidatorsFromAssembly(typeof(RegisterValidators).Assembly);
            services.AddValidatorsFromAssembly(typeof(LoginValidators).Assembly);

            var JwtSettings = new JwtSettings();

            configuration.GetSection(nameof(JwtSettings)).Bind(JwtSettings);

            services.AddSingleton(JwtSettings);

            services.AddScoped<IBaseHandler, BaseHandler>();
            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped(typeof(IAccountService), typeof(AccountService));

            services.AddScoped(typeof(IAuthenticationService), typeof(AuthenticationService));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));


            return services;
        }
    }
}
