using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Specifications.Notifications
{
    public class NotificationsForUserSpec : BaseSpecification<Notification, int>
    {
        public NotificationsForUserSpec(string userid, string? sort, int pageindex, int pagesize) : base(

            p => p.UserId == userid


            )
        {
            AddIncludes();

            switch (sort)
            {
                default:
                    AddOrderByDescending(p => p.CreatedAt);
                    break;
            }
            ApplyPagination((pageindex - 1) * pagesize, pagesize);





        }

        private protected override void AddIncludes()
        {
            base.AddIncludes();
            Includes.Add(p => p.User!); // Assuming Notification has a User navigation property
            Includes.Add(p => p.TriggerUser!); // Assuming Notification has a TriggerUser navigation property

        }
    }
}
