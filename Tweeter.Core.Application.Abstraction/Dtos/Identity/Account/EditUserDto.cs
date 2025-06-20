using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Core.Application.Abstraction.Dtos.Identity.Account
{
	public class EditUserDto
	{
		public required string Id { get; set; }
		public required string FullName { get; set; }
		public required string Email { get; set; }
		public string? PhoneNumber { get; set; }
		public IFormFile? ProfilePictureUrl { get; set; }
	}
}
