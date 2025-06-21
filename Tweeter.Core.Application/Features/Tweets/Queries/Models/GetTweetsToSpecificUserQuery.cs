using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Tweets;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Tweets.Queries.Models
{
	public class GetTweetsToSpecificUserQuery : IRequest<Response<List<TweetToReturnDto>>>
	{
		public string UserId { get; set; }
		public GetTweetsToSpecificUserQuery(string userId)
		{
			UserId = userId;
		}
	}
}
