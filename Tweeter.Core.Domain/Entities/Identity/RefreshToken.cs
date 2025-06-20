using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Core.Domain.Entities.Identity
{
	[Owned]
	public class RefreshToken
	{
		public required string Token { get; set; }
		public DateTime CreatedOn { get; set; }
		public DateTime ExpireOn { get; set; }

		public DateTime? RevokedOn { get; set; }
		public bool IsExpired => DateTime.UtcNow >= ExpireOn;
		public bool IsActive => RevokedOn is null && !IsExpired;

	}
}
