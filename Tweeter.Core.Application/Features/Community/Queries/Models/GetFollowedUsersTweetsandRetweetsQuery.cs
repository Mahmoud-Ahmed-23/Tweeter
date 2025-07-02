using MediatR;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Community.Queries.Models
{
	public class GetFollowedUsersTweetsandRetweetsQuery : IRequest<Response<Pagination<RetweetToReturnDto>>>
	{
		public SpecParams SpecParams { get; set; }

		public GetFollowedUsersTweetsandRetweetsQuery(SpecParams specParams)
		{
			SpecParams = specParams;
		}
	}
}
