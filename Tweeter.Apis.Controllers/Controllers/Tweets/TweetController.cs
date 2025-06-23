using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Apis.Controllers.Controllers.Base;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Tweets;
using Tweeter.Core.Application.Features.Messages.Queries.Models;
using Tweeter.Core.Application.Features.Tweets.Commands.Models;
using Tweeter.Core.Application.Features.Tweets.Queries.Models;
using Tweeter.Core.Domain.AppMateData;

namespace Tweeter.Apis.Controllers.Controllers.Tweets
{
	public class TweetController : BaseApiController
	{
		[Authorize]
		[HttpGet(Router.TweetRouting.GetTweetsToSpecificUser)]
		public async Task<ActionResult<List<TweetToReturnDto>>> GetTweetsToSpecificUser([FromQuery] SpecParams specParams)
		{
			var result = await mediator.Send(new GetTweetsToSpecificUserQuery(specParams));
			return NewResult(result);
		}

		[Authorize]
		[HttpGet(Router.TweetRouting.GetTweetById)]
		public async Task<ActionResult<TweetToReturnDto>> GetTweetById([FromRoute] int id)
		{
			var result = await mediator.Send(new GetTweetByIdQuery(id));
			return NewResult(result);
		}

		[Authorize]
		[HttpPost(Router.TweetRouting.CreateTweet)]
		public async Task<ActionResult<TweetToReturnDto>> CreateTweet([FromForm] CreateTweetDto tweetDto)
		{
			var result = await mediator.Send(new CreateTweetCommand { TweetDto = tweetDto });
			return NewResult(result);
		}

		[Authorize]
		[HttpPut(Router.TweetRouting.UpdateTweet)]
		public async Task<ActionResult<TweetToReturnDto>> UpdateTweet([FromRoute] int id, [FromForm] UpdateTweetDto tweetDto)
		{
			var result = await mediator.Send(new UpdateTweetCommand(id, tweetDto));
			return NewResult(result);
		}

		[Authorize]
		[HttpDelete(Router.TweetRouting.DeleteTweet)]
		public async Task<ActionResult<string>> DeleteTweet([FromRoute] int id)
		{
			var result = await mediator.Send(new DeleteTweetCommand(id));
			return NewResult(result);
		}
	}
}
