using Microsoft.AspNetCore.Mvc;
using Tweeter.Apis.Controllers.Controllers.Base;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
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
        [HttpPost(Router.AccountRouting.SendCode)]
        public async Task<ActionResult<SuccessDto>> ForgetPassword([FromBody] ForgetPasswordByEmailDto emailDto)
        {
            var result = await mediator.Send(new ForgetPasswordCommand() { ForgetPasswordByEmailDto = emailDto });
            return NewResult(result);
        }
        [HttpPost(Router.AccountRouting.VerifyCode)]
        public async Task<ActionResult<SuccessDto>> VerifyCode([FromBody] ResetCodeConfirmationByEmailDto resetCodeDto)
        {
            var result = await mediator.Send(new VerifiyCodeByEmailCommand() { ResetCodeConfirmationByEmailDto = resetCodeDto });
            return NewResult(result);
        }
    }

}
