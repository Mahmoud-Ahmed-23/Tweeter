using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Infrastructure.Persistence._Data.Configurations.Base;

namespace Tweeter.Infrastructure.Persistence._Data.Configurations.Data
{
    internal class TweetHashtagConfiguration : BaseEntityConfigurations<TweetHashtag, int>
    {
        public override void Configure(EntityTypeBuilder<TweetHashtag> builder)
        {
            base.Configure(builder);
            builder.HasKey(th => new { th.Id, th.HashtagId });

            builder.HasOne(th => th.Tweet)
                  .WithMany(t => t.TweetHashtags)
                  .HasForeignKey(th => th.Id)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(th => th.Hashtag)
                  .WithMany(h => h.TweetHashtags)
                  .HasForeignKey(th => th.HashtagId)
                  .OnDelete(DeleteBehavior.Cascade);

        }
    }


}
