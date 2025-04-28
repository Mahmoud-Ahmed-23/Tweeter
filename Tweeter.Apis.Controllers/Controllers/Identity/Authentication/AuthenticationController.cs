using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Apis.Controllers.Controllers.Base;
using Tweeter.Core.Application.Features.Identity.Authentication.Command.Models;
using Tweeter.Core.Domain.AppMateData;

namespace Tweeter.Apis.Controllers.Controllers.Identity.Authentication
{
	public class AuthenticationController : BaseApiController
	{

		[HttpPost(Router.AuthenticationRouting.Login)]
		public async Task<IActionResult> Login([FromBody] LoginCommand command)
		{
			var result = await mediator.Send(command);
			return NewResult(result);
		}
	}
}
