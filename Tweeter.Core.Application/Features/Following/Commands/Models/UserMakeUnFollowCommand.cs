using MediatR;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Following.Commands.Models
{
    public class UserMakeUnFollowCommand : IRequest<Response<bool>>
    {
        public string FolloweeId { get; set; }
        public UserMakeUnFollowCommand(string followeeId)
        {
            FolloweeId = followeeId;
        }
    }
}
