using MediatR;
using Tweeter.Core.Application.Abstraction.Dtos.Hashtags;
using Tweeter.Core.Application.Abstraction.Services.Hashtags;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Hashtages.Queries.Models;

namespace Tweeter.Core.Application.Features.Hashtages.Queries.Handlers
{
    public class HashtagQueryHandler : BaseHandler,
        IRequestHandler<GetHashtagQuery, Response<HashtagToReturn>>,
        IRequestHandler<GetTopFiveHashtagsBasedOnCountOfTweetsQuery, Response<IEnumerable<HashtagToReturn>>>
    {
        private readonly IHashtageService hashtageService;

        public HashtagQueryHandler(IHashtageService hashtageService)
        {
            this.hashtageService = hashtageService;
        }
        public async Task<Response<HashtagToReturn>> Handle(GetHashtagQuery request, CancellationToken cancellationToken)
        {
            var result = await hashtageService.GetByIdAsync(request.Id);
            return await HandleResultAsync(Task.FromResult(result));
        }

        public async Task<Response<IEnumerable<HashtagToReturn>>> Handle(GetTopFiveHashtagsBasedOnCountOfTweetsQuery request, CancellationToken cancellationToken)
        {
            var result = await hashtageService.GetTopFiveHashtagsBasedOnCountOfTweetsAsync();
            return await HandleResultAsync(Task.FromResult(result));
        }
    }
}
