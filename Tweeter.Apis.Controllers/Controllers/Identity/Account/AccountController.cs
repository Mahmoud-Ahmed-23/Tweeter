using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Apis.Controllers.Controllers.Base;
using Tweeter.Core.Application.Abstraction.Dtos.Identity;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Core.Application.Features.Identity.Account.Command.Models;
using Tweeter.Core.Domain.AppMateData;

namespace Tweeter.Apis.Controllers.Controllers.Identity.Account
{
	public class AccountController : BaseApiController
	{
		[HttpPost(Router.AccountRouting.Register)]
		public async Task<ActionResult<ReturnUserDto>> Register([FromBody] RegisterDto registerDto)
		{
			var result = await mediator.Send(new RegisterCommand() { RegisterDto = registerDto });
			return NewResult(result);
		}
	}
}
