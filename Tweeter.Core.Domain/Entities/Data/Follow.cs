using Tweeter.Core.Domain.Common;
using Tweeter.Core.Domain.Entities.Identity;

namespace Tweeter.Core.Domain.Entities.Data
{
    public class Follow : BaseAuditableEntity<int>
    {
        public string FollowerId { get; set; }
        public string FolloweeId { get; set; }
        //public DateTime FollowDate { get; set; }

        //Navigation properties
        public virtual ApplicationUser Follower { get; set; }
        public virtual ApplicationUser Followee { get; set; }
    }
}
