using MediatR;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Application.Abstraction.Services.Identity.Authentication;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Identity.Authentication.Command.Models;

namespace Tweeter.Core.Application.Features.Identity.Authentication.Command.Handlers
{
    internal class AuthenticationCommandHandler
        : BaseHandler,
        IRequestHandler<LoginCommand, Response<ReturnUserDto>>,
        IRequestHandler<ResetPasswordCommand, Response<ReturnUserDto>>
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

        public async Task<Response<ReturnUserDto>> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
        {
            var result = await _authenticationService.ResetPasswordByEmailAsync(request.ResetPasswordByEmailDto);
            return await HandleResultAsync(Task.FromResult(result));
        }
    }
}
