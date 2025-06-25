using MediatR;
using Tweeter.Core.Application.Abstraction.Dtos.Hashtags;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Hashtages.Queries.Models
{
    public class GetHashtagQuery : IRequest<Response<HashtagToReturn>>
    {
        public int Id { get; set; }
        public GetHashtagQuery(int id)
        {
            Id = id;
        }
    }
}
