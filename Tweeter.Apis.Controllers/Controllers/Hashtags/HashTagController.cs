using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tweeter.Apis.Controllers.Controllers.Base;
using Tweeter.Core.Application.Abstraction.Dtos.Hashtags;
using Tweeter.Core.Application.Features.Hashtages.Commands.Models;
using Tweeter.Core.Domain.AppMateData;

namespace Tweeter.Apis.Controllers.Controllers.Hashtags
{
    [Authorize]
    public class HashTagController : BaseApiController
    {
        [HttpPost(Router.HashtagRouting.CreateHashtag)]
        public async Task<ActionResult<bool>> CreateHashtag([FromBody] HashtagDto hashtagDto)
        {
            var result = await mediator.Send(new CreateHashtagCommand() { HashtagDto = hashtagDto });
            return NewResult(result);
        }

    }
}
