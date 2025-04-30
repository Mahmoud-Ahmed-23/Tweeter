using MediatR;
using Microsoft.AspNetCore.Http;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Application.Abstraction.Services.Identity.Authentication;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Identity.Authentication.Command.Models;

namespace Tweeter.Core.Application.Features.Identity.Authentication.Command.Handlers
{
    internal class AuthenticationCommandHandler
        : BaseHandler,
        IRequestHandler<LoginCommand, Response<ReturnUserDto>>,
        IRequestHandler<ResetPasswordCommand, Response<ReturnUserDto>>,
        IRequestHandler<ChangePasswordCommand, Response<ChangePasswordToReturn>>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationCommandHandler(IAuthenticationService authenticationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _authenticationService = authenticationService;
            _httpContextAccessor = httpContextAccessor;
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

        public async Task<Response<ChangePasswordToReturn>> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;


            var result = await _authenticationService.ChangePasswordAsync(user!, request.ChangePasswordDto);
            return await HandleResultAsync(Task.FromResult(result));
        }
    }
}
