using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Core.Application.Abstraction.Dtos.Identity.Account
{
	public class ConfirmUserEmailDto
	{
		public string Email { get; set; }
		public int Code { get; set; }
	}
}
