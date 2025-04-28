using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Core.Domain.Entities.Identity
{
	public class ApplicationUser : IdentityUser
	{
		public required string FullName { get; set; }

	}
}
