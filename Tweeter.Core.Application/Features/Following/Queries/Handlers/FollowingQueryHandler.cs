using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Tweeter.Core.Application.Abstraction.Dtos.Following;
using Tweeter.Core.Application.Abstraction.Services.Following;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Following.Queries.Models;

namespace Tweeter.Core.Application.Features.Following.Queries.Handlers
{
    public class FollowingQueryHandler : BaseHandler,
        IRequestHandler<GetCountFollersQuery, Response<int>>,
        IRequestHandler<GetCountFollowingQuery, Response<int>>,
        IRequestHandler<IsFollowingQuery, Response<bool>>,
        IRequestHandler<GetFollwersQuery, Response<List<UsersToReturn>>>
    {
        private readonly IFollowService _followService;
        private readonly IHttpContextAccessor _contextAccessor;

        public FollowingQueryHandler(IFollowService followService, IHttpContextAccessor contextAccessor)
        {
            _followService = followService;
            _contextAccessor = contextAccessor;
        }
        public async Task<Response<int>> Handle(GetCountFollersQuery request, CancellationToken cancellationToken)
        {
            var userId = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.PrimarySid);

            var result = await _followService.GetFollowerCountAsync(userId!);
            return await HandleResultAsync(Task.FromResult(result));
        }

        public async Task<Response<int>> Handle(GetCountFollowingQuery request, CancellationToken cancellationToken)
        {
            var userId = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.PrimarySid);
            var result = await _followService.GetFollowingCountAsync(userId!);
            return await HandleResultAsync(Task.FromResult(result));
        }

        public async Task<Response<bool>> Handle(IsFollowingQuery request, CancellationToken cancellationToken)
        {
            var userId = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.PrimarySid);
            var result = await _followService.IsFollowingAsync(userId!, request.FolloweeId);
            return await HandleResultAsync(Task.FromResult(result));
        }

        public async Task<Response<List<UsersToReturn>>> Handle(GetFollwersQuery request, CancellationToken cancellationToken)
        {

            var userId = _contextAccessor.HttpContext?.User?.FindFirstValue(ClaimTypes.PrimarySid);
            var result = await _followService.GetFollowersAsync(userId!);
            return await HandleResultAsync(Task.FromResult(result));
        }
    }
}
