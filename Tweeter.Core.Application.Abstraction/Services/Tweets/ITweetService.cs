using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Tweets;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Abstraction.Services.Tweets
{
	public interface ITweetService
	{
		Task<Result<TweetToReturnDto>> CreateTweetAsync(CreateTweetDto tweetDto);

		Task<TweetToReturnDto> GetTweetByIdAsync(string tweetId);

		Task<Result<List<TweetToReturnDto>>> GetTweetsByUserIdAsync(string userId);

	}
}
