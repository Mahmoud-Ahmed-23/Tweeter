using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Apis.Controllers.Controllers.Base;
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
		[HttpPost(Router.TweetRouting.CreateTweet)]
		public async Task<ActionResult<TweetToReturnDto>> CreateTweet([FromForm] CreateTweetDto tweetDto)
		{
			var result = await mediator.Send(new CreateTweetCommand { TweetDto = tweetDto });
			return NewResult(result);
		}

		[Authorize]
		[HttpGet(Router.TweetRouting.GetTweetsToSpecificUser)]
		public async Task<ActionResult<List<TweetToReturnDto>>> GetTweetsToSpecificUser([FromQuery] string userId)
		{
			var result = await mediator.Send(new GetTweetsToSpecificUserQuery(userId));
			return NewResult(result);
		}
	}
}
