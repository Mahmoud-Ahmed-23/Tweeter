using MediatR;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Tweets;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Tweets.Queries.Models
{
	public class GetAllTweetsQuery : IRequest<Response<Pagination<TweetToReturnDto>>>
	{
		public GetAllTweetsQuery(SpecParams specParams)
		{
			SpecParams = specParams;
		}
		public SpecParams SpecParams { get; set; }
	}
}
