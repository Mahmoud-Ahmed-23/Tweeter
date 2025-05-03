using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tweeter.Core.Domain.Common;

namespace Tweeter.Infrastructure.Persistence._Data.Configurations.Base
{
    internal class BaseEntityConfigurations<TEntity, TKey> : IEntityTypeConfiguration<TEntity>
      where TEntity : BaseEntity<TKey> where TKey : IEquatable<TKey>
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {

            builder.Property(e => e.Id).ValueGeneratedOnAdd();

        }
    }
}
