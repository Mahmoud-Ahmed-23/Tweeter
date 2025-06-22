using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Specifications.Notifications
{
    public class UnreadNotificationsForUserCountSpec : BaseSpecification<Notification, int>
    {
        public UnreadNotificationsForUserCountSpec(string userid)
            : base(p => p.UserId == userid && !p.IsRead)
        {
        }

    }
}
