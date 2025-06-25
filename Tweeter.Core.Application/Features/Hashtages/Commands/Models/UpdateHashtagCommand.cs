using MediatR;
using Tweeter.Core.Application.Abstraction.Dtos.Hashtags;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Hashtages.Commands.Models
{
    public class UpdateHashtagCommand : IRequest<Response<HashtagToReturn>>
    {

        public int Id { get; set; }

        public HashtagDto HashtagDto { get; set; }

        public UpdateHashtagCommand(int id, HashtagDto hashtagDto)
        {
            Id = id;
            HashtagDto = hashtagDto;
        }
    }
}
