using MediatR;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Following.Queries.Models
{
    public class IsFollowingQuery : IRequest<Response<bool>>
    {
        public string FolloweeId { get; set; }
        public IsFollowingQuery(string followeeId)
        {
            FolloweeId = followeeId;
        }


    }
}
