using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Abstraction.Services.Community
{
	public interface ICommunityService
	{
		Task<Result<TweetToReturnDto>> CreateTweetAsync(CreateTweetDto tweetDto);

		Task<Result<TweetToReturnDto>> GetTweetByIdAsync(int tweetId);

		Task<Result<Pagination<TweetToReturnDto>>> GetAllTweetsAsync(SpecParams specParams);

		Task<Result<Pagination<TweetToReturnDto>>> GetFollowedUsersTweetsAsync(SpecParams specParams);

		Task<Result<Pagination<TweetToReturnDto>>> GetTweetsByUserIdAsync(string userId, SpecParams specParams);

		Task<Result<TweetToReturnDto>> UpdateTweetAsync(int id, UpdateTweetDto tweetDto);

		Task<Result<string>> DeleteTweetAsync(int id);


		Task<Result<string>> LikeTweetAsync(int tweetId);


		Task<Result<RetweetToReturnDto>> RetweetAsync(int tweetId, string? content);
		Task<Result<RetweetToReturnDto>> GetRetweetAsync(int tweetId);
		//Task<Result<Pagination<RetweetToReturnDto>>> GetRetweetsByUserIdAsync(string userId, SpecParams specParams);
		//Task<Result<Pagination<RetweetToReturnDto>>> GetRetweetsByTweetIdAsync(int tweetId, SpecParams specParams);

		Task<Result<string>> UnretweetAsync(int tweetId);
	}
}
