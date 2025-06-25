using FluentValidation;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Tweeter.Core.Application.Abstraction.Services.Chats;
using Tweeter.Core.Application.Abstraction.Services.Emails;
using Tweeter.Core.Application.Abstraction.Services.Following;
using Tweeter.Core.Application.Abstraction.Services.Hashtags;
using Tweeter.Core.Application.Abstraction.Services.Identity.Account;
using Tweeter.Core.Application.Abstraction.Services.Identity.Authentication;
using Tweeter.Core.Application.Abstraction.Services.Notifications;
using Tweeter.Core.Application.Abstraction.Services.Tweets;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Behaviors;
using Tweeter.Core.Application.Features.Identity.Account.Command.Validators;
using Tweeter.Core.Application.Features.Identity.Authentication.Command.Validators;
using Tweeter.Core.Application.Mapping;
using Tweeter.Core.Application.Services.Chats;
using Tweeter.Core.Application.Services.Emails;
using Tweeter.Core.Application.Services.Following;
using Tweeter.Core.Application.Services.Hashtags;
using Tweeter.Core.Application.Services.Identity.Account;
using Tweeter.Core.Application.Services.Identity.Authentication;
using Tweeter.Core.Application.Services.Notifications;
using Tweeter.Core.Application.Services.Tweets;
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

            #region Registration Services
            services.AddScoped<IBaseHandler, BaseHandler>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped(typeof(INotificationService), typeof(NotificationService));
            services.AddScoped(typeof(IHashtageService), typeof(HashtageService));

            services.AddScoped(typeof(IAccountService), typeof(AccountService));

            services.AddScoped(typeof(IAuthenticationService), typeof(AuthenticationService));
            services.AddScoped(typeof(IChatService), typeof(ChatService));
            services.AddScoped(typeof(IFollowService), typeof(FollowService));
            services.AddScoped(typeof(ITweetService), typeof(TweetService));

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));


            #endregion


            services.Configure<MailSettings>(configuration.GetSection("MailSettings"));

            services.AddHttpContextAccessor();

            services.AddSignalR();


            // add mapping for the application

            services.AddAutoMapper(typeof(MappingProfile));




            return services;
        }
    }
}
