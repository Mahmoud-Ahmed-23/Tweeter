using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Core.Application.Abstraction.Dtos.Messages;
using Tweeter.Core.Application.Abstraction.Services.Community;
using Tweeter.Core.Domain.Contracts.Infrastructure;
using Tweeter.Core.Domain.Contracts.Persistence;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Core.Domain.Entities.Identity;
using Tweeter.Core.Domain.Specifications.Tweets;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Services.Community
{
	internal class CommunityService : ICommunityService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IAttachmentService _attachmentService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IConfiguration _configuration;

		public CommunityService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, IAttachmentService attachmentService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_attachmentService = attachmentService;
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
		}

		public async Task<Result<TweetToReturnDto>> CreateTweetAsync(CreateTweetDto tweetDto)
		{

			if (tweetDto.Content is null || tweetDto.ImageUrl is null)
			{
				return Result<TweetToReturnDto>.Fail("Content and ImageUrl cannot be null.", ErrorType.BadRequest);
			}

			var mappedTweet = _mapper.Map<Tweet>(tweetDto);

			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);

			if (string.IsNullOrEmpty(userId))
			{
				return Result<TweetToReturnDto>.Fail("User ID cannot be null or empty.", ErrorType.Unauthorized);
			}
			mappedTweet.UserId = userId;

			if (tweetDto.ImageUrl is not null)
			{
				var uploadedImageUrl = await _attachmentService.UploadAsynce(tweetDto.ImageUrl, "TweetsPictures");

				if (uploadedImageUrl is not null)
				{
					mappedTweet.ImageUrl = uploadedImageUrl;
				}
				else
				{
					mappedTweet.ImageUrl = null;
				}
			}

			var repo = _unitOfWork.GetRepository<Tweet, int>();

			await repo.AddAsync(mappedTweet);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
			{
				return Result<TweetToReturnDto>.Fail("Failed to send message", ErrorType.Unexpected);
			}

			var tweetToReturn = _mapper.Map<TweetToReturnDto>(mappedTweet);

			return Result<TweetToReturnDto>.Success(tweetToReturn);
		}

		public async Task<Result<string>> DeleteTweetAsync(int id)
		{
			var repo = _unitOfWork.GetRepository<Tweet, int>();

			var tweet = await repo.GetAsync(id);

			if (tweet is null)
			{
				return Result<string>.Fail("Tweet not found.", ErrorType.NotFound);
			}

			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);

			if (string.IsNullOrEmpty(userId) || userId != tweet!.UserId)
			{
				return Result<string>.Fail("User ID cannot be null or empty.", ErrorType.Unauthorized);
			}

			repo.Delete(tweet);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
			{
				return Result<string>.Fail("Failed to delete tweet", ErrorType.Unexpected);
			}

			return Result<string>.Success("Tweet deleted successfully.");
		}

		public async Task<Result<Pagination<TweetToReturnDto>>> GetAllTweetsAsync(SpecParams specParams)
		{
			var specs = new TweetsForAllSpec(specParams.PageIndex, specParams.PageSize);

			var repo = _unitOfWork.GetRepository<Tweet, int>();

			var tweets = await repo.GetAllWithSpecAsync(specs);

			if (tweets is null || !tweets.Any())
			{
				return Result<Pagination<TweetToReturnDto>>.Fail("No tweets found.", ErrorType.NotFound);
			}

			var countSpec = new TweetsForAllCountSpec();

			var totalCount = await repo.GetCountAsync(countSpec);

			var data = _mapper.Map<List<TweetToReturnDto>>(tweets);

			return Result<Pagination<TweetToReturnDto>>.Success(new Pagination<TweetToReturnDto>(specParams.PageIndex, specParams.PageSize, totalCount) { Data = data });
		}

		public async Task<Result<Pagination<TweetToReturnDto>>> GetFollowedUsersTweetsAsync(SpecParams specParams)
		{
			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);

			if (string.IsNullOrEmpty(userId))
			{
				return Result<Pagination<TweetToReturnDto>>.Fail("User ID cannot be null or empty.", ErrorType.Unauthorized);
			}

			var specs = new TweetsForFollowedUsersSpec(userId, specParams.PageIndex, specParams.PageSize);

			var repo = _unitOfWork.GetRepository<Tweet, int>();

			var tweets = await repo.GetAllWithSpecAsync(specs);

			if (tweets is null || !tweets.Any())
			{
				return Result<Pagination<TweetToReturnDto>>.Fail("No tweets found for followed users.", ErrorType.NotFound);
			}

			var countSpec = new TweetsForFollowedUsersCountSpec(userId);

			var totalCount = await repo.GetCountAsync(countSpec);

			var data = _mapper.Map<List<TweetToReturnDto>>(tweets);

			return Result<Pagination<TweetToReturnDto>>.Success(new Pagination<TweetToReturnDto>(specParams.PageIndex, specParams.PageSize, totalCount) { Data = data });
		}

		public async Task<Result<TweetToReturnDto>> GetTweetByIdAsync(int tweetId)
		{
			var repo = _unitOfWork.GetRepository<Tweet, int>();

			var tweet = await repo.GetAsync(tweetId);

			if (tweet is null)
			{
				return Result<TweetToReturnDto>.Fail("Tweet not found.", ErrorType.NotFound);
			}

			var tweetToReturn = _mapper.Map<TweetToReturnDto>(tweet);

			return Result<TweetToReturnDto>.Success(tweetToReturn);
		}

		public async Task<Result<Pagination<TweetToReturnDto>>> GetTweetsByUserIdAsync(string userId, SpecParams specParams)
		{
			var specs = new TweetsForUserSpec(userId, specParams.PageIndex, specParams.PageSize);

			var repo = _unitOfWork.GetRepository<Tweet, int>();

			var tweets = await repo.GetAllWithSpecAsync(specs);

			if (tweets is null || !tweets.Any())
			{
				return Result<Pagination<TweetToReturnDto>>.Fail("No tweets found for the user.", ErrorType.NotFound);
			}

			var countSpec = new TweetsForUserCountSpec(userId);

			var totalCount = await repo.GetCountAsync(countSpec);

			var data = _mapper.Map<List<TweetToReturnDto>>(tweets);

			return Result<Pagination<TweetToReturnDto>>.Success(new Pagination<TweetToReturnDto>(specParams.PageIndex, specParams.PageSize, totalCount) { Data = data });
		}

		public async Task<Result<TweetToReturnDto>> UpdateTweetAsync(int id, UpdateTweetDto tweetDto)
		{
			var repo = _unitOfWork.GetRepository<Tweet, int>();

			var tweet = await repo.GetAsync(id);

			if (tweet is null)
			{
				return Result<TweetToReturnDto>.Fail("Tweet not found.", ErrorType.NotFound);
			}

			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);

			if (string.IsNullOrEmpty(userId) || userId != tweet!.UserId)
			{
				return Result<TweetToReturnDto>.Fail("User ID cannot be null or empty.", ErrorType.Unauthorized);
			}

			var mappedTweet = _mapper.Map(tweetDto, tweet);

			if (tweetDto.ImageUrl is not null)
			{
				var uploadedImageUrl = await _attachmentService.UploadAsynce(tweetDto.ImageUrl, "TweetsPictures");

				if (uploadedImageUrl is not null)
				{
					mappedTweet.ImageUrl = uploadedImageUrl;
				}
				else
				{
					mappedTweet.ImageUrl = null;
				}
			}

			if (mappedTweet.Content is null || mappedTweet.ImageUrl is null)
			{
				return Result<TweetToReturnDto>.Fail("Content and ImageUrl cannot be null.", ErrorType.BadRequest);
			}

			repo.Update(mappedTweet);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
			{
				return Result<TweetToReturnDto>.Fail("Failed to update tweet", ErrorType.Unexpected);
			}

			var tweetToReturn = _mapper.Map<TweetToReturnDto>(mappedTweet);

			return Result<TweetToReturnDto>.Success(tweetToReturn);
		}

		public async Task<Result<string>> LikeTweetAsync(int tweetId)
		{
			var repo = _unitOfWork.GetRepository<Tweet, int>();

			var tweet = await repo.GetAsync(tweetId);

			if (tweet is null)
			{
				return Result<string>.Fail("Tweet not found.", ErrorType.NotFound);
			}

			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);

			if (string.IsNullOrEmpty(userId))
			{
				return Result<string>.Fail("User ID cannot be null or empty.", ErrorType.Unauthorized);
			}

			if (tweet.Likes.Any(l => l.UserId == userId))
			{
				await UnlikeTweetAsync(tweetId);

				// If the user already liked the tweet, we remove the like and return a success message.

				return Result<string>.Success("Tweet unliked successfully.");
			}

			tweet.Likes.Add(new Like { UserId = userId, TweetId = tweetId });

			repo.Update(tweet);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
			{
				return Result<string>.Fail("Failed to like tweet", ErrorType.Unexpected);
			}

			return Result<string>.Success("Tweet liked successfully.");
		}

		private async Task<Result<string>> UnlikeTweetAsync(int tweetId)
		{
			var repo = _unitOfWork.GetRepository<Tweet, int>();

			var tweet = await repo.GetAsync(tweetId);

			if (tweet is null)
			{
				return Result<string>.Fail("Tweet not found.", ErrorType.NotFound);
			}

			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);

			if (string.IsNullOrEmpty(userId))
			{
				return Result<string>.Fail("User ID cannot be null or empty.", ErrorType.Unauthorized);
			}

			var like = tweet.Likes.FirstOrDefault(l => l.UserId == userId);

			if (like is null)
			{
				await LikeTweetAsync(tweetId);

				// If the user has not liked the tweet, we add the like and return a success message.

				return Result<string>.Success("Tweet liked successfully.");
			}

			tweet.Likes.Remove(like);

			repo.Update(tweet);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
			{
				return Result<string>.Fail("Failed to unlike tweet", ErrorType.Unexpected);
			}

			return Result<string>.Success("Tweet unliked successfully.");
		}

		public async Task<Result<RetweetToReturnDto>> RetweetAsync(int tweetId, string? content)
		{
			var tweetRepo = _unitOfWork.GetRepository<Tweet, int>();

			var tweet = await tweetRepo.GetAsync(tweetId);

			if (tweet is null)
			{
				return Result<RetweetToReturnDto>.Fail("Tweet not found.", ErrorType.NotFound);
			}

			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);

			if (string.IsNullOrEmpty(userId))
			{
				return Result<RetweetToReturnDto>.Fail("User ID cannot be null or empty.", ErrorType.Unauthorized);
			}

			var retweetRepo = _unitOfWork.GetRepository<Retweet, int>();

			var retweet = new Retweet
			{
				OriginalTweetId = tweetId,
				UserId = userId,
				Comment = content ?? string.Empty,
				RetweetedAt = DateTime.UtcNow,
				//OriginalTweet = _mapper.Map<Tweet>(tweet)
			};

			await retweetRepo.AddAsync(retweet);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
			{
				return Result<RetweetToReturnDto>.Fail("Failed to retweet", ErrorType.Unexpected);
			}

			var retweetToReturn = _mapper.Map<RetweetToReturnDto>(retweet);

			return Result<RetweetToReturnDto>.Success(retweetToReturn);
		}


		public Task<Result<string>> UnretweetAsync(int tweetId)
		{
			throw new NotImplementedException();
		}

		public async Task<Result<RetweetToReturnDto>> GetRetweetAsync(int tweetId)
		{
			var retweetRepo = _unitOfWork.GetRepository<Retweet, int>();

			var retweet = await retweetRepo.GetAsync(tweetId);

			if (retweet is null)
			{
				return Result<RetweetToReturnDto>.Fail("Retweet not found.", ErrorType.NotFound);
			}

			var retweetToReturn = _mapper.Map<RetweetToReturnDto>(retweet);

			return Result<RetweetToReturnDto>.Success(retweetToReturn);
		}
	}
}
