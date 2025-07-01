using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Community.Commands.Models
{
	public class UnRetweetCommand : IRequest<Response<string>>
	{
		public int TweetId { get; set; }
		public UnRetweetCommand(int tweetId)
		{
			TweetId = tweetId;
		}
	}
}
