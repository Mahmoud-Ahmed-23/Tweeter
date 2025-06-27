using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Community.Queries.Models
{
	public class GetTweetsToSpecificUserQuery : IRequest<Response<Pagination<TweetToReturnDto>>>
	{
		public SpecParams SpecParams { get; set; }
		public GetTweetsToSpecificUserQuery(SpecParams specParams)
		{
			SpecParams = specParams;
		}
	}
}
