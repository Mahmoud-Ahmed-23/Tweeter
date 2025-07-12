using Microsoft.EntityFrameworkCore;
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
		public string? NormalizedComment { get; set; }

		// Navigation properties
		public virtual ApplicationUser User { get; set; }
		public virtual Tweet OriginalTweet { get; set; }
		public virtual ICollection<RetweetLikes> Likes { get; set; }
		public virtual ICollection<Reply> Replies { get; set; }
		public virtual ICollection<TweetHashtag> TweetHashtags { get; set; }
		public virtual ICollection<Mention> Mentions { get; set; }
	}
}
