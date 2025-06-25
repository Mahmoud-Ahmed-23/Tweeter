using MediatR;
using Tweeter.Core.Application.Abstraction.Services.Hashtags;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Hashtages.Commands.Models;

namespace Tweeter.Core.Application.Features.Hashtages.Commands.Handlers
{
    public class HashTagCommandHandler : BaseHandler, IRequestHandler<CreateHashtagCommand, Response<bool>>
    {
        private readonly IHashtageService _hashtageService;

        public HashTagCommandHandler(IHashtageService hashtageService)
        {
            _hashtageService = hashtageService;
        }
        public async Task<Response<bool>> Handle(CreateHashtagCommand request, CancellationToken cancellationToken)
        {
            var result = await _hashtageService.CreateAsync(request.HashtagDto);

            return await HandleResultAsync(Task.FromResult(result));

        }
    }
}
