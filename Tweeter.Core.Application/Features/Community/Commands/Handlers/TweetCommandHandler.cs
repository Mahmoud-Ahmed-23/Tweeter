using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Application.Abstraction.Services.Community;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Community.Commands.Models;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Features.Community.Commands.Handlers
{
	public class TweetCommandHandler
		: BaseHandler,
		IRequestHandler<CreateTweetCommand, Response<TweetToReturnDto>>,
		IRequestHandler<UpdateTweetCommand, Response<TweetToReturnDto>>,
		IRequestHandler<DeleteTweetCommand, Response<string>>,
		IRequestHandler<LikeTweetCommand, Response<string>>,
		IRequestHandler<RetweetCommand, Response<RetweetToReturnDto>>
	{
		private readonly ICommunityService _tweetService;

		public TweetCommandHandler(ICommunityService tweetService, IHttpContextAccessor httpContextAccessor)
		{
			_tweetService = tweetService;
		}

		public async Task<Response<TweetToReturnDto>> Handle(CreateTweetCommand request, CancellationToken cancellationToken)
		{

			var result = await _tweetService.CreateTweetAsync(request.TweetDto);

			return await HandleResultAsync(Task.FromResult(result));
		}

		public async Task<Response<TweetToReturnDto>> Handle(UpdateTweetCommand request, CancellationToken cancellationToken)
		{
			var result = await _tweetService.UpdateTweetAsync(request.Id, request.TweetDto);

			return await HandleResultAsync(Task.FromResult(result));
		}

		public async Task<Response<string>> Handle(DeleteTweetCommand request, CancellationToken cancellationToken)
		{
			var result = await _tweetService.DeleteTweetAsync(request.Id);

			return await HandleResultAsync(Task.FromResult(result));
		}

		public async Task<Response<string>> Handle(LikeTweetCommand request, CancellationToken cancellationToken)
		{
			var result = await _tweetService.LikeTweetAsync(request.TweetId);

			return await HandleResultAsync(Task.FromResult(result));
		}

		public async Task<Response<RetweetToReturnDto>> Handle(RetweetCommand request, CancellationToken cancellationToken)
		{
			var result = await _tweetService.RetweetAsync(request.TweetId, request.Content);

			return await HandleResultAsync(Task.FromResult(result));
		}
	}
}
