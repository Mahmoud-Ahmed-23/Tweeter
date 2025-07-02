using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Infrastructure.Persistence._Data.Configurations.Base;

namespace Tweeter.Infrastructure.Persistence._Data.Configurations.Data
{
	internal class RetweetLikesConfiguration : BaseAuditableEntityConfigurations<RetweetLikes, int>
	{
		public override void Configure(EntityTypeBuilder<RetweetLikes> builder)
		{
			base.Configure(builder);
			builder.HasIndex(l => new { l.UserId, l.RetweetId }).IsUnique();

			// Relationships
			builder.HasOne(l => l.User)
				  .WithMany(u => u.RetweetLikes)
				  .HasForeignKey(l => l.UserId)
				  .OnDelete(DeleteBehavior.Restrict);

			builder.HasOne(l => l.Retweet)
				  .WithMany(t => t.Likes)
				  .HasForeignKey(l => l.RetweetId)
				  .OnDelete(DeleteBehavior.Cascade);
		}
	}
}
