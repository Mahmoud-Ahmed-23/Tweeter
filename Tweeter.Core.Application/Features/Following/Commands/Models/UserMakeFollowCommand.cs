using MediatR;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Following.Commands.Models
{
    public class UserMakeFollowCommand : IRequest<Response<bool>>
    {
        public string FolloweeId { get; set; }
        public UserMakeFollowCommand(string followeeId)
        {
            FolloweeId = followeeId;
        }
    }
}
