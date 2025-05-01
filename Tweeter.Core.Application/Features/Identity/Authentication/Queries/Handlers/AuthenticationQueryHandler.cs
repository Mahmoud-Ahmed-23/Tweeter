using MediatR;
using Microsoft.AspNetCore.Http;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Application.Abstraction.Services.Identity.Authentication;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Identity.Authentication.Queries.Models;

namespace Tweeter.Core.Application.Features.Identity.Authentication.Queries.Handlers
{
    public class AuthenticationQueryHandler : BaseHandler, IRequestHandler<GetCurrentUserQuery, Response<ReturnUserDto>>
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthenticationQueryHandler(IAuthenticationService authenticationService,
            IHttpContextAccessor httpContextAccessor)
        {
            _authenticationService = authenticationService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Response<ReturnUserDto>> Handle(GetCurrentUserQuery request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var result = await _authenticationService.GetCurrentUser(user!);
            return await HandleResultAsync(Task.FromResult(result));

        }
    }

}
