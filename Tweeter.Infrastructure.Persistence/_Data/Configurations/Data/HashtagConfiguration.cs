using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Infrastructure.Persistence._Data.Configurations.Base;

namespace Tweeter.Infrastructure.Persistence._Data.Configurations.Data
{
    internal class HashtagConfiguration : BaseAuditableEntityConfigurations<Hashtag, int>
    {
        public override void Configure(EntityTypeBuilder<Hashtag> builder)
        {
            base.Configure(builder);
            builder.Property(h => h.TagName).IsRequired().HasMaxLength(50);
            builder.HasIndex(h => h.TagName).IsUnique();



        }
    }
}
