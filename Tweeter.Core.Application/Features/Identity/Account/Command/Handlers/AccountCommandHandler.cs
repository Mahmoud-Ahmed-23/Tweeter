using MediatR;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Application.Abstraction.Services.Identity.Account;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Identity.Account.Command.Models;

namespace Tweeter.Core.Application.Features.Identity.Account.Command.Handlers
{
    internal class AccountCommandHandler :
        BaseHandler,
        IRequestHandler<RegisterCommand, Response<ReturnUserDto>>,
        IRequestHandler<ForgetPasswordCommand, Response<SuccessDto>>,
        IRequestHandler<VerifiyCodeByEmailCommand, Response<SuccessDto>>

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

        public async Task<Response<SuccessDto>> Handle(ForgetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _accountService.SendCodeByEmailAsync(request.ForgetPasswordByEmailDto);
            return await HandleResultAsync(Task.FromResult(result));
        }

        public async Task<Response<SuccessDto>> Handle(VerifiyCodeByEmailCommand request, CancellationToken cancellationToken)
        {
            var result = await _accountService.VerifyCodeByEmailAsync(request.ResetCodeConfirmationByEmailDto);
            return await HandleResultAsync(Task.FromResult(result));
        }
    }
}
