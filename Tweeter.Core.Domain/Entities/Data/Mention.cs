using Tweeter.Core.Domain.Common;
using Tweeter.Core.Domain.Entities.Identity;

namespace Tweeter.Core.Domain.Entities.Data
{
    public class Mention : BaseEntity<int>
    {
        public int TweetId { get; set; }
        public string MentionedUserId { get; set; }

        // Navigation properties
        public virtual Tweet Tweet { get; set; }
        public virtual ApplicationUser MentionedUser { get; set; }
    }
}
