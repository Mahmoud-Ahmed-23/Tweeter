using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Infrastructure.Persistence._Data.Configurations.Base;

namespace Tweeter.Infrastructure.Persistence._Data.Configurations.Data
{
    internal class ReplyConfiguration : BaseAuditableEntityConfigurations<Reply, int>
    {
        public override void Configure(EntityTypeBuilder<Reply> builder)
        {
            base.Configure(builder);

            builder.HasOne(r => r.ParentTweet)
           .WithMany(t => t.Replies)
           .HasForeignKey(r => r.TweetId)
           .OnDelete(DeleteBehavior.Cascade); // Delete replies if tweet is deleted

            // Relationship with User (many replies by one user)
            builder.HasOne(r => r.User)
                  .WithMany(u => u.Replies)
                  .HasForeignKey(r => r.UserId)
                  .OnDelete(DeleteBehavior.Restrict); // Prevent user deletion if they have replies
        }
    }
}
