using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Specifications.Following
{
    public class FollowersCountSpecification : BaseSpecification<Follow, int>
    {
        public FollowersCountSpecification(string Userid)
            : base(p => p.FolloweeId == Userid)
        {
            AddIncludes();
        }
        public FollowersCountSpecification(int id) : base(id)
        {
            AddIncludes();
        }
        private protected override void AddIncludes()
        {
            base.AddIncludes();
            // No additional includes needed for count

        }
    }
}
