using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Identity;
using Tweeter.Core.Application.Abstraction.Services.Identity.Authentication;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Identity.Authentication.Command.Models;

namespace Tweeter.Core.Application.Features.Identity.Authentication.Command.Handlers
{
	internal class AuthenticationCommandHandler
		: BaseHandler,
		IRequestHandler<LoginCommand, Response<ReturnUserDto>>
	{
		private readonly IAuthenticationService _authenticationService;

		public AuthenticationCommandHandler(IAuthenticationService authenticationService)
		{
			_authenticationService = authenticationService;
		}

		public async Task<Response<ReturnUserDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			var result = await _authenticationService.Login(request.Email, request.Password);

			return await HandleResultAsync(Task.FromResult(result));
		}
	}
}
