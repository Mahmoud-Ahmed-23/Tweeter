using Tweeter.Core.Domain.Common;
using Tweeter.Core.Domain.Entities.Identity;

namespace Tweeter.Core.Domain.Entities.Data
{
    public class Retweet : BaseAuditableEntity<int>
    {
        public string UserId { get; set; }
        public int OriginalTweetId { get; set; }
        public DateTime RetweetedAt { get; set; }
        public string? Comment { get; set; }

        // Navigation properties
        public virtual ApplicationUser User { get; set; }
        public virtual Tweet OriginalTweet { get; set; }
		public virtual ICollection<Like> Likes { get; set; }
		public virtual ICollection<Retweet> Retweets { get; set; }
		public virtual ICollection<Reply> Replies { get; set; }
		public virtual ICollection<TweetHashtag> TweetHashtags { get; set; }
		public virtual ICollection<Mention> Mentions { get; set; }
	}
}
