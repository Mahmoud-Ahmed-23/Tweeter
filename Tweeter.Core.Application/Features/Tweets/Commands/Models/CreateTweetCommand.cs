using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Tweets;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Tweets.Commands.Models
{
	public class CreateTweetCommand : IRequest<Response<TweetToReturnDto>>
	{
		public CreateTweetDto TweetDto { get; set; }
	}
}
