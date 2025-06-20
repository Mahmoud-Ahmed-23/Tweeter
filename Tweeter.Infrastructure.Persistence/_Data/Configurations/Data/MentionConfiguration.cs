using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Infrastructure.Persistence._Data.Configurations.Base;

namespace Tweeter.Infrastructure.Persistence._Data.Configurations.Data
{
    internal class MentionConfiguration : BaseEntityConfigurations<Mention, int>
    {
        public override void Configure(EntityTypeBuilder<Mention> builder)
        {
            base.Configure(builder);

            builder.HasOne(m => m.Tweet)
         .WithMany(t => t.Mentions)
         .HasForeignKey(m => m.TweetId)
         .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(m => m.MentionedUser)
                  .WithMany(u => u.Mentions)
                  .HasForeignKey(m => m.MentionedUserId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
