using MediatR;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Messages.Queries.Models
{
    public class MarkMessageAsReadQuery : IRequest<Response<bool>>
    {

        public int MessageId { get; set; }
    }
}
