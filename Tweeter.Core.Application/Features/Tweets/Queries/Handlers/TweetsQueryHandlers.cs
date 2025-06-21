using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Tweets;
using Tweeter.Core.Application.Abstraction.Services.Tweets;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Tweets.Queries.Models;

namespace Tweeter.Core.Application.Features.Tweets.Queries.Handlers
{
	public class TweetsQueryHandlers :
		BaseHandler,
		IRequestHandler<GetTweetsToSpecificUserQuery, Response<List<TweetToReturnDto>>>
	{
		private readonly ITweetService _tweetService;
		public TweetsQueryHandlers(ITweetService tweetService)
		{
			_tweetService = tweetService;
		}
		public async Task<Response<List<TweetToReturnDto>>> Handle(GetTweetsToSpecificUserQuery request, CancellationToken cancellationToken)
		{
			var result = await _tweetService.GetTweetsByUserIdAsync(request.UserId);
			return await HandleResultAsync(Task.FromResult(result));
		}
	}
}
