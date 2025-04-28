using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Core.Application.Abstraction.Dtos.Identity.Account
{
	public record RegisterDto
	{
		public required string FullName { get; set; }
		public required string Email { get; set; }
		public required string Password { get; set; }
		public string? PhoneNumber { get; set; }
		public string? ProfilePictureUrl { get; set; }
		public required string Role { get; set; }

	}
}
