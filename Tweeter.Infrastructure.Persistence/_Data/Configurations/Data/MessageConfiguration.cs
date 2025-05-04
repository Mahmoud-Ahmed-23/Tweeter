using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Infrastructure.Persistence._Data.Configurations.Base;

namespace Tweeter.Infrastructure.Persistence._Data.Configurations.Data
{
    internal class MessageConfiguration : BaseEntityConfigurations<Message, int>
    {
        public override void Configure(EntityTypeBuilder<Message> builder)
        {
            base.Configure(builder);

            builder.Property(m => m.Content)
                   .IsRequired()
                   .HasMaxLength(280); // Assuming a max length for message content
            builder.Property(m => m.SentAt)
                     .IsRequired()
                     .HasDefaultValueSql("GETDATE()"); // Default to current date/time



            // Configure Sender relationship
            builder.HasOne(m => m.Sender)
                  .WithMany(u => u.SentMessages)
                  .HasForeignKey(m => m.SenderId)
                  .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete for sender

            // Configure Receiver relationship
            builder.HasOne(m => m.Receiver)
                  .WithMany(u => u.ReceivedMessages)
                  .HasForeignKey(m => m.ReceiverId)
                  .OnDelete(DeleteBehavior.Restrict); // Prevent cascade delete for receiver
        }
    }
}
