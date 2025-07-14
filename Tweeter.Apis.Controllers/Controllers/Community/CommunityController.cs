using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Apis.Controllers.Controllers.Base;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Features.Messages.Queries.Models;
using Tweeter.Core.Application.Features.Community.Commands.Models;
using Tweeter.Core.Application.Features.Community.Queries.Models;
using Tweeter.Core.Domain.AppMateData;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Apis.Controllers.Filters;

namespace Tweeter.Apis.Controllers.Controllers.Community
{
	public class CommunityController : BaseApiController
	{
		[HttpGet(Router.CommunityRouting.GetTweetsToSpecificUser)]
		public async Task<ActionResult<List<TweetToReturnDto>>> GetTweetsToSpecificUser([FromQuery] SpecParams specParams)
		{
			var result = await mediator.Send(new GetTweetsToSpecificUserQuery(specParams));
			return NewResult(result);
		}

		[HttpGet(Router.CommunityRouting.GetTweetById)]
		public async Task<ActionResult<TweetToReturnDto>> GetTweetById([FromRoute] int id)
		{
			var result = await mediator.Send(new GetTweetByIdQuery(id));
			return NewResult(result);
		}

		[Authorize]
		[HttpPost(Router.CommunityRouting.CreateTweet)]
		public async Task<ActionResult<TweetToReturnDto>> CreateTweet([FromForm] CreateTweetDto tweetDto)
		{
			var result = await mediator.Send(new CreateTweetCommand { TweetDto = tweetDto });
			return NewResult(result);
		}

		[Authorize]
		[HttpPut(Router.CommunityRouting.UpdateTweet)]
		public async Task<ActionResult<TweetToReturnDto>> UpdateTweet([FromRoute] int id, [FromForm] UpdateTweetDto tweetDto)
		{
			var result = await mediator.Send(new UpdateTweetCommand(id, tweetDto));
			return NewResult(result);
		}

		[Authorize]
		[HttpDelete(Router.CommunityRouting.DeleteTweet)]
		public async Task<ActionResult<string>> DeleteTweet([FromRoute] int id)
		{
			var result = await mediator.Send(new DeleteTweetCommand(id));
			return NewResult(result);
		}

		[Cached(600)]
		[HttpGet(Router.CommunityRouting.GetAllTweets)]
		public async Task<ActionResult<Pagination<TweetToReturnDto>>> GetAllTweets([FromQuery] SpecParams specParams)
		{
			var result = await mediator.Send(new GetAllTweetsQuery(specParams));
			return NewResult(result);
		}

		[Cached(600)]
		[Authorize]
		[HttpGet(Router.CommunityRouting.GetTweetsForFollowedUsers)]
		public async Task<ActionResult<Pagination<TweetToReturnDto>>> GetTweetsForFollowedUsers([FromQuery] SpecParams specParams)
		{
			var result = await mediator.Send(new GetFollowedUsersTweetsandRetweetsQuery(specParams));
			return NewResult(result);
		}

		[Authorize]
		[HttpPost(Router.CommunityRouting.LikeTweet)]
		public async Task<ActionResult<string>> LikeTweet([FromRoute] int id)
		{
			var result = await mediator.Send(new LikeTweetCommand(id));
			return NewResult(result);
		}

		[Authorize]
		[HttpPost(Router.CommunityRouting.LikeRetweet)]
		public async Task<ActionResult<string>> LikeRetweet([FromRoute] int id)
		{
			var result = await mediator.Send(new LikeRetweetCommand(id));
			return NewResult(result);
		}

		[Authorize]
		[HttpPost(Router.CommunityRouting.Retweet)]
		public async Task<ActionResult<RetweetToReturnDto>> Retweet([FromRoute] int id, [FromForm] string? content)
		{
			var result = await mediator.Send(new RetweetCommand(id, content));
			return NewResult(result);
		}

		[Authorize]
		[HttpDelete(Router.CommunityRouting.UnRetweet)]
		public async Task<ActionResult<string>> DeleteRetweet([FromRoute] int id)
		{
			var result = await mediator.Send(new UnRetweetCommand(id));
			return NewResult(result);
		}

		[HttpGet(Router.CommunityRouting.GetRetweet)]
		public async Task<ActionResult<RetweetToReturnDto>> GetRetweet([FromRoute] int id)
		{
			var result = await mediator.Send(new GetRetweetQuery(id));
			return NewResult(result);
		}

		[Authorize]
		[HttpPut(Router.CommunityRouting.UpdateRetweet)]
		public async Task<ActionResult<RetweetToReturnDto>> UpdateRetweet([FromRoute] int retweetId, [FromForm] string content)
		{
			var result = await mediator.Send(new UpdateRetweetCommand(retweetId, content));
			return NewResult(result);
		}
	}
}