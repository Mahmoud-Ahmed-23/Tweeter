using Tweeter.Core.Domain.Contracts.Common;

namespace Tweeter.Core.Application.Abstraction.Dtos.Notifications
{
    public class NotificationDto
    {
        public string UserId { get; set; }
        public string TriggerUserId { get; set; }
        public int? TweetId { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        public required string FullNameForUser { get; set; }
        public string? ProfilePictureUrlForUser { get; set; }
        public required string FullNameForTriggerUser { get; set; }
        public string? ProfilePictureUrlForTriggerUser { get; set; }
    }
}
