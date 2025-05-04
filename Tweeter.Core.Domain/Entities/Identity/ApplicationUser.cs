using Microsoft.AspNetCore.Identity;
using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Entities.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public required string FullName { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? ResetCode { get; set; }
        public DateTime? ResetCodeExpiry { get; set; }


        public virtual ICollection<Tweet> Tweets { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Retweet> Retweets { get; set; }
        public virtual ICollection<Reply> Replies { get; set; }
        public virtual ICollection<Follow> Followers { get; set; } // Users who follow this user
        public virtual ICollection<Follow> Following { get; set; } // Users this user follows
        public virtual ICollection<Mention> Mentions { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
        public virtual ICollection<Message> SentMessages { get; set; }
        public virtual ICollection<Message> ReceivedMessages { get; set; }
    }
}
