using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tweeter.Apis.Controllers.Controllers.Base;
using Tweeter.Core.Application.Features.Following.Commands.Models;
using Tweeter.Core.Application.Features.Following.Queries.Models;
using Tweeter.Core.Domain.AppMateData;

namespace Tweeter.Apis.Controllers.Controllers.Following
{
    [Authorize]
    public class FollowingController : BaseApiController
    {
        [HttpPost(Router.FollowingRouting.FollowUser)]
        public async Task<ActionResult<bool>> FollowUser([FromQuery] string followeeid)
        {
            var command = new UserMakeFollowCommand(followeeid);
            var result = await mediator.Send(command);
            return NewResult(result);
        }
        [HttpDelete(Router.FollowingRouting.UnfollowUser)]
        public async Task<ActionResult<bool>> UnFollowUser([FromQuery] string followeeid)
        {
            var command = new UserMakeUnFollowCommand(followeeid);
            var result = await mediator.Send(command);
            return NewResult(result);

        }
        [HttpGet(Router.FollowingRouting.GetCountOfFollowers)]
        public async Task<ActionResult<int>> GetCountOfFollowers()
        {
            var result = await mediator.Send(new GetCountFollersQuery());
            return NewResult(result);
        }
        [HttpGet(Router.FollowingRouting.GetCountOfFollowing)]
        public async Task<ActionResult<int>> GetCountOfFollowing()
        {
            var result = await mediator.Send(new GetCountFollowingQuery());
            return NewResult(result);
        }
        [HttpGet(Router.FollowingRouting.IsFollowing)]
        public async Task<ActionResult<bool>> IsFollowing([FromQuery] string followeeid)
        {
            var result = await mediator.Send(new IsFollowingQuery(followeeid));
            return NewResult(result);
        }
    }
}
