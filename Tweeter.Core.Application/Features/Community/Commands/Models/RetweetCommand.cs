using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Community.Commands.Models
{
	public class RetweetCommand : IRequest<Response<RetweetToReturnDto>>
	{
		public int TweetId { get; set; }
		public string? Content { get; set; }

		public RetweetCommand(int tweetId, string? content)
		{
			TweetId = tweetId;
			Content = content;
		}


	}
}
