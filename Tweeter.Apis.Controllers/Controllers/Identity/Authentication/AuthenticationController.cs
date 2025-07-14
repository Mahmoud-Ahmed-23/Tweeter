using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tweeter.Apis.Controllers.Controllers.Base;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Application.Features.Identity.Authentication.Command.Models;
using Tweeter.Core.Application.Features.Identity.Authentication.Queries.Models;
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

		[HttpPost(Router.AuthenticationRouting.ResetPassword)]
		public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordByEmailDto command)
		{
			var result = await mediator.Send(new ResetPasswordCommand() { ResetPasswordByEmailDto = command });
			return NewResult(result);
		}

		[Authorize]
		[HttpPost(Router.AuthenticationRouting.ChangePassword)]
		public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordDto command)
		{
			var result = await mediator.Send(new ChangePasswordCommand() { ChangePasswordDto = command });
			return NewResult(result);
		}

		[HttpPost(Router.AuthenticationRouting.RefreshToken)]
		public async Task<ActionResult<string>> RefreshToken([FromBody] RefreshTokenCommand command)
		{
			var result = await mediator.Send(command);
			return NewResult(result);
		}

		[HttpPost(Router.AuthenticationRouting.RevokeRefreshToken)]
		public async Task<ActionResult<string>> RevokeRefreshToken([FromBody] RevokeRefreshTokenCommand command)
		{
			var result = await mediator.Send(command);
			return NewResult(result);
		}

		[Authorize]
		[HttpPost(Router.AuthenticationRouting.Logout)]
		public async Task<IActionResult> Logout()
		{
			var result = await mediator.Send(new LougOutCommand());
			return NewResult(result);
		}
		[Authorize]
		[HttpGet(Router.AuthenticationRouting.GetCurrentUser)]
		public async Task<IActionResult> GetCurrentUser()
		{
			var result = await mediator.Send(new GetCurrentUserQuery());
			return NewResult(result);
		}

		[HttpGet(Router.AuthenticationRouting.GetUserProfile)]
		public async Task<ActionResult<UserProfileToReturn>> GetUserProfile([FromQuery] SpecParams specParams)
		{
			var result = await mediator.Send(new GetUserProfileQuery() { SpecParams = specParams });
			return NewResult(result);
		}
	}

}
