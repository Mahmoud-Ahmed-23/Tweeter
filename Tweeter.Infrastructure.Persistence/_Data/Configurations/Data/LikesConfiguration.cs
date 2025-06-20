using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Infrastructure.Persistence._Data.Configurations.Base;

namespace Tweeter.Infrastructure.Persistence._Data.Configurations.Data
{
    internal class LikesConfiguration : BaseAuditableEntityConfigurations<Like, int>
    {
        public override void Configure(EntityTypeBuilder<Like> builder)
        {
            base.Configure(builder);
            builder.HasIndex(l => new { l.UserId, l.TweetId }).IsUnique();

            // Relationships
            builder.HasOne(l => l.User)
                  .WithMany(u => u.Likes)
                  .HasForeignKey(l => l.UserId)
                  .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(l => l.Tweet)
                  .WithMany(t => t.Likes)
                  .HasForeignKey(l => l.TweetId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
