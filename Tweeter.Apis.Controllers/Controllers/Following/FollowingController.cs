using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tweeter.Apis.Controllers.Controllers.Base;
using Tweeter.Core.Application.Features.Following.Commands.Models;
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

    }
}
