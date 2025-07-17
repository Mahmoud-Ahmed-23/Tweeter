using AutoMapper;
using Microsoft.Extensions.Configuration;
using Tweeter.Core.Application.Abstraction.Dtos.Notifications;
using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Application.Mapping.NotificationResolvers
{
    public class TriggerUserNotificationPictureUrlResolver(IConfiguration configuration) : IValueResolver<Notification, NotificationDto, string?>
    {
        public string? Resolve(Notification source, NotificationDto destination, string? destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.TriggerUser!.ProfilePictureUrl))
            {
                return $"{configuration["Urls:TwitterUrl"]}/{source.TriggerUser.ProfilePictureUrl}";
            }
            return string.Empty;
        }
    }
}
