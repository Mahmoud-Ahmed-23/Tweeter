using Tweeter.Core.Domain.Common;
using Tweeter.Core.Domain.Entities.Identity;

namespace Tweeter.Core.Domain.Entities.Data
{
    public class Tweet : BaseAuditableEntity<int>
    {
        public string UserId { get; set; }
        public string Content { get; set; }
        //public int LikeCount { get; set; }

        public string? ImageUrl { get; set; }
        //public int RetweetCount { get; set; }

        //public int ReplyCount { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation properties
        public virtual ApplicationUser User { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Retweet> Retweets { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<TweetHashtag> TweetHashtags { get; set; }
        public virtual ICollection<Mention> Mentions { get; set; }

    }
}
