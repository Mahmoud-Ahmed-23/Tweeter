using MediatR;
using Tweeter.Core.Application.Abstraction.Dtos.Messages;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Messages.Queries.Models
{
    public class GetConversationQuery : IRequest<Response<List<MessageDto>>>
    {
        public string otherUserId { get; set; }

    }
}
