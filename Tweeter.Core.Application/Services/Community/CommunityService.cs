using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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
using Tweeter.Core.Application.Services.Hubs;
using Tweeter.Core.Domain.Contracts.Common;
using Tweeter.Core.Domain.Contracts.Infrastructure;
using Tweeter.Core.Domain.Contracts.Persistence;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Core.Domain.Entities.Identity;
using Tweeter.Core.Domain.Specifications.Retweets;
using Tweeter.Core.Domain.Specifications.Tweets;
using Tweeter.Shared.Results;
using static System.Net.Mime.MediaTypeNames;

namespace Tweeter.Core.Application.Services.Community
{
	internal class CommunityService : ICommunityService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IAttachmentService _attachmentService;
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IConfiguration _configuration;
		private readonly IHubContext<NotificationHub> _hubContext;
		private readonly UserManager<ApplicationUser> _userManager;

		public CommunityService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, IAttachmentService attachmentService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor, IHubContext<NotificationHub> hubContext)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_attachmentService = attachmentService;
			_configuration = configuration;
			_httpContextAccessor = httpContextAccessor;
			_hubContext = hubContext;
			_userManager = userManager;
		}

		public async Task<Result<TweetToReturnDto>> CreateTweetAsync(CreateTweetDto tweetDto)
		{

			if (tweetDto.Content is null && tweetDto.ImageUrl is null)
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



			#region Send Notification

			var followersRepo = _unitOfWork.GetRepository<Follow, int>();

			var followers = await followersRepo.GetAllQueryableAsync()
				.Where(f => f.FolloweeId == userId)
				.Select(f => f.FollowerId)
				.ToListAsync();

			if (followers.Count > 0)
			{
				var notificationRepo = _unitOfWork.GetRepository<Notification, int>();

				var notifications = followers.Select(followerId => new Notification
				{
					TriggerUserId = userId,
					UserId = followerId,
					CreatedAt = DateTime.UtcNow,
					TweetId = mappedTweet.Id
				}).ToList();


				await notificationRepo.AddRangeAsync(notifications);

				var notificationCompleted = await _unitOfWork.CompleteAsync() > 0;

				if (!notificationCompleted)
				{
					return Result<TweetToReturnDto>.Fail("Failed to create notifications", ErrorType.Unexpected);
				}

				var username = (await _userManager.FindByIdAsync(userId))?.FullName;

				foreach (var followerId in followers)
				{
					await _hubContext.Clients
						.User(followerId)
						.SendAsync("ReceiveNotification", $"{username} posted a new tweet");
				}
			}
			#endregion


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
			var specs = new TweetsForAllSpec(specParams.PageIndex, specParams.PageSize, specParams.Search);

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

		public async Task<Result<Pagination<RetweetToReturnDto>>> GetFollowedUsersTweetsandRetweetsAsync(SpecParams specParams)
		{
			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);

			if (string.IsNullOrEmpty(userId))
			{
				return Result<Pagination<RetweetToReturnDto>>.Fail("User ID cannot be null or empty.", ErrorType.Unauthorized);
			}

			var tweets = await TweetsForFollowedUsers(userId, specParams);

			var retweets = await RetweetsForFollowedUsers(userId, specParams);

			if ((tweets is null || !tweets.Any()) && (retweets is null || !retweets.Any()))
			{
				return Result<Pagination<RetweetToReturnDto>>.Fail("No tweets found for followed users.", ErrorType.NotFound);
			}


			var tweetCountSpec = await TweetsForFollowedUsersCount(userId);

			var retweetCountSpec = await RetweetsForFollowedUsersCount(userId);

			var totalCount = tweetCountSpec + retweetCountSpec;

			var data = _mapper.Map<List<RetweetToReturnDto>>(tweets);

			data.AddRange(_mapper.Map<List<RetweetToReturnDto>>(retweets));

			return Result<Pagination<RetweetToReturnDto>>.Success(new Pagination<RetweetToReturnDto>(specParams.PageIndex, specParams.PageSize, totalCount) { Data = data });
		}

		private async Task<IEnumerable<Tweet>> TweetsForFollowedUsers(string userId, SpecParams specParams)
		{
			var specs = new TweetsForFollowedUsersSpec(userId, specParams.PageIndex, specParams.PageSize, specParams.Search);

			var repo = _unitOfWork.GetRepository<Tweet, int>();

			return await repo.GetAllWithSpecAsync(specs);
		}
		private async Task<int> TweetsForFollowedUsersCount(string userId)
		{
			var countSpec = new TweetsForFollowedUsersCountSpec(userId);

			var repo = _unitOfWork.GetRepository<Tweet, int>();

			return await repo.GetCountAsync(countSpec);
		}

		private async Task<IEnumerable<Retweet>> RetweetsForFollowedUsers(string userId, SpecParams specParams)
		{
			var specs = new RetweetsForFollowedUsersSpec(userId, specParams.PageIndex, specParams.PageSize, specParams.Search);

			var repo = _unitOfWork.GetRepository<Retweet, int>();

			return await repo.GetAllWithSpecAsync(specs);
		}

		private async Task<int> RetweetsForFollowedUsersCount(string userId)
		{
			var countSpec = new RetweetsForFollowedUsersCountSpec(userId);

			var repo = _unitOfWork.GetRepository<Retweet, int>();

			return await repo.GetCountAsync(countSpec);
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

			#region Send Notification

			var notification = new Notification()
			{
				CreatedAt = DateTime.UtcNow,
				TriggerUserId = userId, // The user who liked the tweet
				UserId = tweet.UserId,
				NotificationType = NotificationType.Follow,
				TweetId = tweet.Id,
			};

			await _unitOfWork.GetRepository<Notification, int>().AddAsync(notification);

			var notificationCompleted = await _unitOfWork.CompleteAsync() > 0;

			if (!notificationCompleted)
			{
				return Result<string>.Fail("Failed to create notification", ErrorType.Unexpected);
			}

			await _hubContext.Clients.Users(tweet.UserId).SendAsync("ReceiveNotification", $"{tweet.User.FullName} Like Your Tweet");

			#endregion

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

			var retweetRepo = _unitOfWork.GetRepository<Retweet, int>();

			var tweet = await tweetRepo.GetAsync(tweetId);

			var getRetweet = await retweetRepo.GetAsync(tweetId);

			if (getRetweet is not null)
			{
				tweet = getRetweet.OriginalTweet;
			}

			if (tweet is null)
			{
				return Result<RetweetToReturnDto>.Fail("Tweet not found.", ErrorType.NotFound);
			}

			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);

			if (string.IsNullOrEmpty(userId))
			{
				return Result<RetweetToReturnDto>.Fail("User ID cannot be null or empty.", ErrorType.Unauthorized);
			}


			var retweet = new Retweet
			{
				OriginalTweetId = tweet.Id,
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

			#region Send Notification

			var followersRepo = _unitOfWork.GetRepository<Follow, int>();

			var followers = await followersRepo.GetAllQueryableAsync()
				.Where(f => f.FolloweeId == userId)
				.Select(f => f.FollowerId)
				.ToListAsync();

			if (followers.Count > 0)
			{
				var notificationRepo = _unitOfWork.GetRepository<Notification, int>();

				var notifications = followers.Select(followerId => new Notification
				{
					TriggerUserId = userId,
					UserId = followerId,
					CreatedAt = DateTime.UtcNow,
					RetweetId = retweet.Id
				}).ToList();


				await notificationRepo.AddRangeAsync(notifications);

				var notificationCompleted = await _unitOfWork.CompleteAsync() > 0;

				if (!notificationCompleted)
				{
					return Result<RetweetToReturnDto>.Fail("Failed to create notifications", ErrorType.Unexpected);
				}

				var username = (await _userManager.FindByIdAsync(userId))?.FullName;

				foreach (var followerId in followers)
				{
					await _hubContext.Clients
						.User(followerId)
						.SendAsync("ReceiveNotification", $"{username} posted a new tweet");
				}
			}
			#endregion

			var retweetToReturn = _mapper.Map<RetweetToReturnDto>(retweet);

			return Result<RetweetToReturnDto>.Success(retweetToReturn);
		}


		public async Task<Result<string>> UnRetweetAsync(int tweetId)
		{
			var retweetRepo = _unitOfWork.GetRepository<Retweet, int>();

			var retweet = await retweetRepo.GetAsync(tweetId);

			if (retweet is null)
			{
				return Result<string>.Fail("Retweet not found.", ErrorType.NotFound);
			}

			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);

			if (string.IsNullOrEmpty(userId) || userId != retweet.UserId)
			{
				return Result<string>.Fail("User ID cannot be null or empty.", ErrorType.Unauthorized);
			}

			retweetRepo.Delete(retweet);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
			{
				return Result<string>.Fail("Failed to unretweet", ErrorType.Unexpected);
			}

			return Result<string>.Success("Retweet deleted successfully.");
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

		public async Task<Result<string>> LikeRetweetAsync(int retweetId)
		{
			var retweetRepo = _unitOfWork.GetRepository<Retweet, int>();

			var retweet = await retweetRepo.GetAsync(retweetId);

			if (retweet is null)
			{
				return Result<string>.Fail("Retweet not found.", ErrorType.NotFound);
			}

			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);

			if (string.IsNullOrEmpty(userId))
			{
				return Result<string>.Fail("User ID cannot be null or empty.", ErrorType.Unauthorized);
			}

			if (retweet.Likes.Any(l => l.UserId == userId))
			{
				await UnlikeRetweetAsync(retweetId);
				// If the user already liked the retweet, we remove the like and return a success message.
				return Result<string>.Success("Retweet unliked successfully.");
			}

			retweet.Likes.Add(new RetweetLikes { UserId = userId, RetweetId = retweetId });

			retweetRepo.Update(retweet);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
			{
				return Result<string>.Fail("Failed to like retweet", ErrorType.Unexpected);
			}

			#region Send Notification

			var notification = new Notification()
			{
				CreatedAt = DateTime.UtcNow,
				UserId = retweet.UserId,
				TriggerUserId = userId,
				NotificationType = NotificationType.Follow,
				RetweetId = retweet.Id,
			};

			await _unitOfWork.GetRepository<Notification, int>().AddAsync(notification);

			var notificationCompleted = await _unitOfWork.CompleteAsync() > 0;

			if (!notificationCompleted)
			{
				return Result<string>.Fail("Failed to create notification", ErrorType.Unexpected);
			}
			await _hubContext.Clients.Users(retweet.UserId).SendAsync("ReceiveNotification", $"{retweet.User.FullName} Like Your Retweet");


			#endregion

			return Result<string>.Success("Retweet liked successfully.");
		}
		private async Task<Result<string>> UnlikeRetweetAsync(int retweetId)
		{
			var retweetRepo = _unitOfWork.GetRepository<Retweet, int>();

			var retweet = await retweetRepo.GetAsync(retweetId);

			if (retweet is null)
			{
				return Result<string>.Fail("Retweet not found.", ErrorType.NotFound);
			}

			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);

			if (string.IsNullOrEmpty(userId))
			{
				return Result<string>.Fail("User ID cannot be null or empty.", ErrorType.Unauthorized);
			}

			var like = retweet.Likes.FirstOrDefault(l => l.UserId == userId);

			if (like is null)
			{
				await LikeRetweetAsync(retweetId);
				// If the user has not liked the retweet, we add the like and return a success message.
				return Result<string>.Success("Retweet liked successfully.");
			}

			retweet.Likes.Remove(like);

			retweetRepo.Update(retweet);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
			{
				return Result<string>.Fail("Failed to unlike retweet", ErrorType.Unexpected);
			}

			return Result<string>.Success("Retweet unliked successfully.");
		}

		public async Task<Result<RetweetToReturnDto>> UpdateRetweetAsync(int retweetId, string content)
		{
			var retweetRepo = _unitOfWork.GetRepository<Retweet, int>();

			var retweet = await retweetRepo.GetAsync(retweetId);

			if (retweet is null)
			{
				return Result<RetweetToReturnDto>.Fail("Retweet not found.", ErrorType.NotFound);
			}

			var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);

			if (string.IsNullOrEmpty(userId) || userId != retweet.UserId)
			{
				return Result<RetweetToReturnDto>.Fail("User ID cannot be null or empty.", ErrorType.Unauthorized);
			}

			retweet.Comment = content;

			retweetRepo.Update(retweet);

			var completed = await _unitOfWork.CompleteAsync() > 0;

			if (!completed)
			{
				return Result<RetweetToReturnDto>.Fail("Failed to update retweet", ErrorType.Unexpected);
			}

			var retweetToReturn = _mapper.Map<RetweetToReturnDto>(retweet);

			return Result<RetweetToReturnDto>.Success(retweetToReturn);
		}



	}
}
