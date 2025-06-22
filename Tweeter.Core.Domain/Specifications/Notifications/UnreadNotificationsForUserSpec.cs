using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Specifications.Notifications
{
    public class UnreadNotificationsForUserSpec : BaseSpecification<Notification, int>
    {
        public UnreadNotificationsForUserSpec(string userId, string? sort, int pageIndex, int pageSize)
            : base(n => n.UserId == userId && !n.IsRead)
        {
            AddIncludes();
            switch (sort)
            {
                default:
                    AddOrderByDescending(n => n.CreatedAt);
                    break;
            }
            ApplyPagination((pageIndex - 1) * pageSize, pageSize);

        }
        private protected override void AddIncludes()
        {
            base.AddIncludes();
            Includes.Add(n => n.User!); // Assuming Notification has a User navigation property
            Includes.Add(n => n.TriggerUser!); // Assuming Notification has a TriggerUser navigation property
        }
    }

}
