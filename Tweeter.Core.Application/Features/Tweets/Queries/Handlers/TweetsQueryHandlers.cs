using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Notifications;
using Tweeter.Core.Application.Abstraction.Dtos.Tweets;
using Tweeter.Core.Application.Abstraction.Services.Tweets;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Tweets.Queries.Models;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Features.Tweets.Queries.Handlers
{
	public class TweetsQueryHandlers :
		BaseHandler,
		IRequestHandler<GetTweetsToSpecificUserQuery, Response<Pagination<TweetToReturnDto>>>,
		IRequestHandler<GetTweetByIdQuery, Response<TweetToReturnDto>>,
		IRequestHandler<GetAllTweetsQuery, Response<Pagination<TweetToReturnDto>>>,
		IRequestHandler<GetFollowedUsersTweetsQuery, Response<Pagination<TweetToReturnDto>>>
	{
		private readonly ITweetService _tweetService;
		private readonly IHttpContextAccessor _httpContextAccessor;

		public TweetsQueryHandlers(ITweetService tweetService, IHttpContextAccessor httpContextAccessor)
		{
			_tweetService = tweetService;
			_httpContextAccessor = httpContextAccessor;
		}
		public async Task<Response<Pagination<TweetToReturnDto>>> Handle(GetTweetsToSpecificUserQuery request, CancellationToken cancellationToken)
		{
			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);

			if (string.IsNullOrEmpty(userId))
			{
				return Fail<Pagination<TweetToReturnDto>>("User ID cannot be null or empty.", ErrorType.Unauthorized);
			}

			var result = await _tweetService.GetTweetsByUserIdAsync(userId, request.SpecParams);

			return await HandleResultAsync(Task.FromResult(result));
		}

		public async Task<Response<TweetToReturnDto>> Handle(GetTweetByIdQuery request, CancellationToken cancellationToken)
		{
			var result = await _tweetService.GetTweetByIdAsync(request.TweetId);

			return await HandleResultAsync(Task.FromResult(result));
		}

		public async Task<Response<Pagination<TweetToReturnDto>>> Handle(GetAllTweetsQuery request, CancellationToken cancellationToken)
		{
			var result = await _tweetService.GetAllTweetsAsync(request.SpecParams);
			return await HandleResultAsync(Task.FromResult(result));
		}

		public async Task<Response<Pagination<TweetToReturnDto>>> Handle(GetFollowedUsersTweetsQuery request, CancellationToken cancellationToken)
		{
			var result = await _tweetService.GetFollowedUsersTweetsAsync(request.SpecParams);
			return await HandleResultAsync(Task.FromResult(result));
		}
	}
}
