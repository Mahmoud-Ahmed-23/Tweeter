using MediatR;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Tweets;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Hashtages.Queries.Models
{
    public class GetTweetsBasedOnHashtagIdQuery : IRequest<Response<Pagination<TweetToReturnDto>>>
    {
        public int Id { get; set; }
        public SpecParams SpecParams { get; set; }
        public GetTweetsBasedOnHashtagIdQuery(int id, SpecParams specParams)
        {
            Id = id;
            SpecParams = specParams;
        }
    }

}
