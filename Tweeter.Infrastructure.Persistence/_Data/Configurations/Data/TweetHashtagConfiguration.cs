using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Infrastructure.Persistence._Data.Configurations.Data
{
    internal class TweetHashtagConfiguration : IEntityTypeConfiguration<TweetHashtag>
    {
        public void Configure(EntityTypeBuilder<TweetHashtag> builder)
        {
            builder.HasKey(th => new { th.TweetId, th.HashtagId });

            builder.Property(p => p.Id).UseIdentityColumn(1, 1);
            builder.Property(p => p.Id).HasColumnName("Container");

            builder.HasOne(th => th.Tweet)
                  .WithMany(t => t.TweetHashtags)
                  .HasForeignKey(th => th.TweetId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(th => th.Hashtag)
                  .WithMany(h => h.TweetHashtags)
                  .HasForeignKey(th => th.HashtagId)
                  .OnDelete(DeleteBehavior.Cascade);


        }
    }


}
