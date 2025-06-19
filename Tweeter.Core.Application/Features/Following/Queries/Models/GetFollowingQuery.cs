using MediatR;
using Tweeter.Core.Application.Abstraction.Dtos.Following;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Following.Queries.Models
{
    public class GetFollowingQuery : IRequest<Response<List<UsersToReturn>>>
    {

    }
}
