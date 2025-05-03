using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Infrastructure.Persistence._Data.Configurations.Base;

namespace Tweeter.Infrastructure.Persistence._Data.Configurations.Data
{
    internal class HashtagConfiguration : BaseEntityConfigurations<Hashtag, int>
    {
        public override void Configure(EntityTypeBuilder<Hashtag> builder)
        {
            base.Configure(builder);
            builder.Property(h => h.Tag).IsRequired().HasMaxLength(50);
            builder.HasIndex(h => h.Tag).IsUnique();

        }
    }
}
