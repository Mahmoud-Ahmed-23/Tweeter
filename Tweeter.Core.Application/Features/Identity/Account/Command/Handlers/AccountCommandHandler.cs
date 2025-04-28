using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Identity;
using Tweeter.Core.Application.Abstraction.Services.Identity.Account;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Identity.Account.Command.Models;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Features.Identity.Account.Command.Handlers
{
	internal class AccountCommandHandler :
		BaseHandler,
		IRequestHandler<RegisterCommand, Response<ReturnUserDto>>
	{
		private readonly IAccountService _accountService;

		public AccountCommandHandler(IAccountService accountService)
		{
			_accountService = accountService;
		}

		public async Task<Response<ReturnUserDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
		{
			var result = await _accountService.Register(request.RegisterDto);

			return await HandleResultAsync(Task.FromResult(result));
		}
	}
}
