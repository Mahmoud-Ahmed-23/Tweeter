using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Infrastructure.Persistence._Data.Configurations.Base;

namespace Tweeter.Infrastructure.Persistence._Data.Configurations.Data
{
    internal class NotificationConfiguration : BaseEntityConfigurations<Notification, int>
    {
        public override void Configure(EntityTypeBuilder<Notification> builder)
        {
            base.Configure(builder);
            builder.Property(n => n.NotificationType).IsRequired();
            builder.Property(n => n.IsRead).HasDefaultValue(false);
            builder.Property(n => n.CreatedAt).HasDefaultValueSql("GETDATE()");

            // Relationships
            builder.HasOne(n => n.Tweet)
             .WithMany()
             .HasForeignKey(n => n.TweetId)
             .OnDelete(DeleteBehavior.NoAction); // Changed from Cascade

            // Other relationships remain the same
            builder.HasOne(n => n.User)
                  .WithMany(u => u.Notifications)
                  .HasForeignKey(n => n.UserId)
                  .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(n => n.TriggerUser)
                  .WithMany()
                  .HasForeignKey(n => n.TriggerUserId)
                  .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
