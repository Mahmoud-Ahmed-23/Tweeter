using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Infrastructure.Persistence._Data.Configurations.Base;

namespace Tweeter.Infrastructure.Persistence._Data.Configurations.Data
{
    internal class FollowConfiguration : BaseAuditableEntityConfigurations<Follow, int>
    {
        public override void Configure(EntityTypeBuilder<Follow> builder)
        {
            base.Configure(builder);


            builder.HasIndex(f => new { f.FollowerId, f.FolloweeId }).IsUnique();



            // Relationships
            builder.HasOne(f => f.Follower)
          .WithMany(u => u.Following)
          .HasForeignKey(f => f.FollowerId)
          .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(f => f.Followee)
         .WithMany(u => u.Followers)
         .HasForeignKey(f => f.FolloweeId)
         .OnDelete(DeleteBehavior.Restrict);

        }
    }
}
