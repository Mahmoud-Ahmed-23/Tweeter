using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Identity;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Identity.Authentication.Command.Models
{
	public class LoginCommand : IRequest<Response<ReturnUserDto>>
	{
		public required string Email { get; set; }
		public required string Password { get; set; }
	}
}
