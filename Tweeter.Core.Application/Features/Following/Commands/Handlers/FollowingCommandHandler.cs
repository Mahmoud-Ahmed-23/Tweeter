using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Tweeter.Core.Application.Abstraction.Services.Following;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Following.Commands.Models;

namespace Tweeter.Core.Application.Features.Following.Commands.Handlers
{
    public class FollowingCommandHandler : BaseHandler,
        IRequestHandler<UserMakeFollowCommand, Response<bool>>,
        IRequestHandler<UserMakeUnFollowCommand, Response<bool>>
    {
        private readonly IFollowService _followService;
        private readonly IHttpContextAccessor _contextAccessor;

        public FollowingCommandHandler(IFollowService followService, IHttpContextAccessor contextAccessor)
        {
            _followService = followService;
            _contextAccessor = contextAccessor;
        }
        public async Task<Response<bool>> Handle(UserMakeFollowCommand request, CancellationToken cancellationToken)
        {

            var followerId = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.PrimarySid);


            var result = await _followService.FollowUserAsync(followerId!, request.FolloweeId);
            return await HandleResultAsync(Task.FromResult(result));



        }

        public async Task<Response<bool>> Handle(UserMakeUnFollowCommand request, CancellationToken cancellationToken)
        {
            var followerId = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.PrimarySid);
            var result = await _followService.UnfollowUserAsync(followerId!, request.FolloweeId);
            return await HandleResultAsync(Task.FromResult(result));
        }
    }
}
