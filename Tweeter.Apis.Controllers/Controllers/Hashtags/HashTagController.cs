using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tweeter.Apis.Controllers.Controllers.Base;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Hashtags;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Application.Features.Hashtages.Commands.Models;
using Tweeter.Core.Application.Features.Hashtages.Queries.Models;
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
        [HttpPut(Router.HashtagRouting.UpdateHashtag)]
        public async Task<ActionResult<HashtagToReturn>> UpdateHashtag([FromRoute] int id, [FromBody] HashtagDto hashtagDto)
        {
            var result = await mediator.Send(new UpdateHashtagCommand(id, hashtagDto));
            return NewResult(result);
        }
        [HttpGet(Router.HashtagRouting.GetHashtagById)]
        public async Task<ActionResult<HashtagToReturn>> GetHashtagById([FromRoute] int id)
        {
            var result = await mediator.Send(new GetHashtagQuery(id));
            return NewResult(result);
        }
        [HttpDelete(Router.HashtagRouting.DeleteHashtag)]
        public async Task<ActionResult<bool>> DeleteHashtag([FromRoute] int id)
        {
            var result = await mediator.Send(new DeleteHashtagCommand(id));
            return NewResult(result);
        }
        [HttpGet(Router.HashtagRouting.GetTopFiveHashtages)]
        public async Task<ActionResult<IEnumerable<HashtagToReturn>>> GetTopFiveHashtages()
        {
            var result = await mediator.Send(new GetTopFiveHashtagsBasedOnCountOfTweetsQuery());
            return NewResult(result);
        }
        [HttpGet(Router.HashtagRouting.GetTweetsByHashtagId)]
        public async Task<ActionResult<Pagination<TweetToReturnDto>>> GetTweetsByHashtagId([FromRoute] int id, [FromQuery] SpecParams specParams)
        {
            var result = await mediator.Send(new GetTweetsBasedOnHashtagIdQuery(id, specParams));
            return NewResult(result);
        }
    }
}
