using MediatR;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Following.Queries.Models
{
    public class GetCountFollowingQuery : IRequest<Response<int>>
    {
    }
}
