using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tweeter.Apis.Controllers.Controllers.Base;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
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

    }

}
