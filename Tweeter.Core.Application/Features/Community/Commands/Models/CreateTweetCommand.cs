using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Community.Commands.Models
{
	public class CreateTweetCommand : IRequest<Response<TweetToReturnDto>>
	{
		public CreateTweetDto TweetDto { get; set; }
	}
}
