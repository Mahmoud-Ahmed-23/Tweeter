using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Infrastructure.Persistence._Data.Configurations.Base;

namespace Tweeter.Infrastructure.Persistence._Data.Configurations.Data
{
    internal class TweetConfiguration : BaseAuditableEntityConfigurations<Tweet, int>
    {
        public override void Configure(EntityTypeBuilder<Tweet> builder)
        {
            base.Configure(builder);
            builder.Property(t => t.Content).IsRequired().HasMaxLength(280);



            // Relationships
            builder.HasOne(t => t.User)
                  .WithMany(u => u.Tweets)
                  .HasForeignKey(t => t.UserId)
                  .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
