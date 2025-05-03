using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Identity.Authentication.Command.Models
{
	public class RefreshTokenCommand : IRequest<Response<ReturnUserDto>>
	{
		public string RefreshToken { get; set; }
		public string Token { get; set; }

	}
}
