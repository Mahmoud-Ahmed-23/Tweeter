using MediatR;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Notifications.Queries.Models
{
    public class GetCountOfUnreadableNotificationsQuery : IRequest<Response<int>>
    {
    }
}
