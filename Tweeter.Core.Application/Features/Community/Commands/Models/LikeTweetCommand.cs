using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Community.Commands.Models
{
	public class LikeTweetCommand : IRequest<Response<string>>
	{
		public int TweetId { get; set; }
		public LikeTweetCommand(int tweetId)
		{
			TweetId = tweetId;
		}
	}
}
