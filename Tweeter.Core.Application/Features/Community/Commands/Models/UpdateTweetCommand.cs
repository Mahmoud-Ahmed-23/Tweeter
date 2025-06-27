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
	public class UpdateTweetCommand : IRequest<Response<TweetToReturnDto>>
	{

		public int Id { get; set; }
		public UpdateTweetDto TweetDto { get; set; }

		public UpdateTweetCommand(int id, UpdateTweetDto tweetDto)
		{
			Id = id;
			TweetDto = tweetDto;
		}
	}
}
