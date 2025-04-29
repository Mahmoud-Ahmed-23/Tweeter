using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Domain.Entities.Identity;

namespace Tweeter.Infrastructure.Persistence._Data.Configurations.Identity
{
	public class ApplicationUserConfigurations : IEntityTypeConfiguration<ApplicationUser>
	{
		public void Configure(EntityTypeBuilder<ApplicationUser> builder)
		{

			builder.Property(u => u.Id)
				.ValueGeneratedOnAdd();

			builder.Property(u => u.FullName)
				.IsRequired()
				.HasMaxLength(100);

			builder.Property(u => u.ProfilePictureUrl)
				.IsRequired(false)
				.HasMaxLength(200);

			builder.Property(u => u.CreatedAt)
				.HasDefaultValueSql("GETUTCDATE()")
				.ValueGeneratedOnAdd();
		}
	}
}
