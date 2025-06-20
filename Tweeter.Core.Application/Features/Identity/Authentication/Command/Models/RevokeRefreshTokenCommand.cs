using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Identity.Authentication.Command.Models
{
	public class RevokeRefreshTokenCommand : IRequest<Response<bool>>
	{
		public string Token { get; set; }
		public string RefreshToken { get; set; }

	}
}
