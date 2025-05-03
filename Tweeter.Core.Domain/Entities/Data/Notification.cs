using Tweeter.Core.Domain.Common;
using Tweeter.Core.Domain.Contracts.Common;
using Tweeter.Core.Domain.Entities.Identity;

namespace Tweeter.Core.Domain.Entities.Data
{
    public class Notification : BaseEntity<int>
    {
        public string UserId { get; set; }
        public string TriggerUserId { get; set; }
        public int? TweetId { get; set; }
        public NotificationType NotificationType { get; set; }
        public bool IsRead { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public virtual ApplicationUser? User { get; set; } // Recipient
        public virtual ApplicationUser? TriggerUser { get; set; } // Who caused the notification
        public virtual Tweet Tweet { get; set; } // Related tweet if applicable
    }
}
