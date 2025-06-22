using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Specifications.Notifications
{
    public class NotificationsForUserCountSpec : BaseSpecification<Notification, int>
    {
        public NotificationsForUserCountSpec(string userid)
            : base(p => p.UserId == userid)
        {
        }

    }
}
