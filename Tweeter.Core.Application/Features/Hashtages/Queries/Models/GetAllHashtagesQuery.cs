using MediatR;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Hashtags;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Hashtages.Queries.Models
{
    public class GetAllHashtagesQuery : IRequest<Response<Pagination<HashtagToReturn>>>
    {
        public SpecParams SpecParams { get; set; }

        public GetAllHashtagesQuery(SpecParams specParams)
        {
            SpecParams = specParams;
        }

    }
}
