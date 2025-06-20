using Tweeter.Core.Domain.Common;
using Tweeter.Core.Domain.Entities.Identity;

namespace Tweeter.Core.Domain.Entities.Data
{
    public class Like : BaseAuditableEntity<int>
    {
        public int LikeId { get; set; }
        public string UserId { get; set; }
        public int TweetId { get; set; }

        // Navigation properties
        public virtual ApplicationUser User { get; set; }
        public virtual Tweet Tweet { get; set; }
    }
}
