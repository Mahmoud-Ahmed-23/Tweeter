using MediatR;
using Tweeter.Core.Application.Abstraction.Dtos.Hashtags;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Hashtages.Commands.Models
{
    public class CreateHashtagCommand : IRequest<Response<bool>>
    {
        public HashtagDto HashtagDto { get; set; }
    }
}
