using MediatR;
using Tweeter.Core.Application.Abstraction.Dtos.Hashtags;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Hashtages.Queries.Models
{
    public class GetTopFiveHashtagsBasedOnCountOfTweetsQuery : IRequest<Response<IEnumerable<HashtagToReturn>>>
    {
    }
}
