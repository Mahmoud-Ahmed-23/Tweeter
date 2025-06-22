using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Core.Application.Abstraction.Dtos.Messages;
using Tweeter.Core.Application.Abstraction.Dtos.Tweets;
using Tweeter.Core.Application.Abstraction.Services.Tweets;
using Tweeter.Core.Domain.Contracts.Infrastructure;
using Tweeter.Core.Domain.Contracts.Persistence;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Core.Domain.Entities.Identity;
using Tweeter.Core.Domain.Specifications.Tweets;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Services.Tweets
{
	internal class TweetService : ITweetService
	{
		private readonly IUnitOfWork _unitOfWork;
		private readonly IMapper _mapper;
		private readonly IAttachmentService _attachmentService;
		private readonly IConfiguration _configuration;

		public TweetService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager, IAttachmentService attachmentService, IConfiguration configuration)
		{
			_unitOfWork = unitOfWork;
			_mapper = mapper;
			_attachmentService = attachmentService;
			_configuration = configuration;
		}

		public async Task<Result<TweetToReturnDto>> CreateTweetAsync(CreateTweetDto tweetDto)
		{
			var mappedTweet = _mapper.Map<Tweet>(tweetDto);
			mappedTweet.UserId = tweetDto.UserId;

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
	}
}
