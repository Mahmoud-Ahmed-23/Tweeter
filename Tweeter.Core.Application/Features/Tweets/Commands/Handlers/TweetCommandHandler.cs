using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Tweets;
using Tweeter.Core.Application.Abstraction.Services.Tweets;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Tweets.Commands.Models;

namespace Tweeter.Core.Application.Features.Tweets.Commands.Handlers
{
	public class TweetCommandHandler
		: BaseHandler,
		IRequestHandler<CreateTweetCommand, Response<TweetToReturnDto>>
	{
		private readonly ITweetService _tweetService;
		public TweetCommandHandler(ITweetService tweetService)
		{
			_tweetService = tweetService;
		}

		public async Task<Response<TweetToReturnDto>> Handle(CreateTweetCommand request, CancellationToken cancellationToken)
		{

			var result = await _tweetService.CreateTweetAsync(request.TweetDto);

			return await HandleResultAsync(Task.FromResult(result));
		}
	}
}
