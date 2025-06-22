using AutoMapper;
using Microsoft.Extensions.Configuration;
using Tweeter.Core.Application.Abstraction.Dtos.Notifications;
using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Application.Mapping
{
    public class UserNotificationPictureUrlResolver(IConfiguration configuration) : IValueResolver<Notification, NotificationDto, string?>
    {
        public string? Resolve(Notification source, NotificationDto destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.User!.ProfilePictureUrl))
            {
                return $"{configuration["Urls:ApiBaseUrl"]}/{source.User.ProfilePictureUrl}";
            }
            return string.Empty;
        }
    }
}
