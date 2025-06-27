using MediatR;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Community.Queries.Models
{
	public class GetFollowedUsersTweetsQuery : IRequest<Response<Pagination<TweetToReturnDto>>>
	{
		public SpecParams SpecParams { get; set; }

		public GetFollowedUsersTweetsQuery(SpecParams specParams)
		{
			SpecParams = specParams;
		}
	}
}
