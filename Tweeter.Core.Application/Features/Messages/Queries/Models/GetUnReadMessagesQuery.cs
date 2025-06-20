using MediatR;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Messages.Queries.Models
{
    public class GetUnReadMessagesQuery : IRequest<Response<int>>
    {

    }
}
