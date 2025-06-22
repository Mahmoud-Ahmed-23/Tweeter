using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Tweets;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Abstraction.Services.Tweets
{
	public interface ITweetService
	{
		Task<Result<TweetToReturnDto>> CreateTweetAsync(CreateTweetDto tweetDto);

		Task<Result<TweetToReturnDto>> GetTweetByIdAsync(int tweetId);

		Task<Result<Pagination<TweetToReturnDto>>> GetTweetsByUserIdAsync(string userId, SpecParams specParams);

	}
}
