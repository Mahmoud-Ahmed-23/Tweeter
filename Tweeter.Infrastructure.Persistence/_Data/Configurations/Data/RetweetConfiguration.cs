using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Infrastructure.Persistence._Data.Configurations.Base;

namespace Tweeter.Infrastructure.Persistence._Data.Configurations.Data
{
    internal class RetweetConfiguration : BaseAuditableEntityConfigurations<Retweet, int>
    {
        public override void Configure(EntityTypeBuilder<Retweet> builder)
        {
            base.Configure(builder);
            builder.HasIndex(r => new { r.UserId, r.OriginalTweetId }).IsUnique();

            // Relationships
            builder.HasOne(r => r.User)
                  .WithMany(u => u.Retweets)
                  .HasForeignKey(r => r.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.OriginalTweet)
                  .WithMany(t => t.Retweets)
                  .HasForeignKey(r => r.OriginalTweetId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
