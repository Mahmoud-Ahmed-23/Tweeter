using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto
{
	public record ReturnUserDto
	{
		public string Id { get; set; }
		public string FullName { get; set; }
		public string Email { get; set; }
		public string? ProfilePictureUrl { get; set; }
		public string? PhoneNumber { get; set; }
		public string? Role { get; set; }
		public string? Token { get; set; }
	}
}
