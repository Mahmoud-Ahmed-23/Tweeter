using MediatR;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Notifications.Commands.Models
{
    public class DeleteSpecififNotificationQuery : IRequest<Response<bool>>
    {
        public DeleteSpecififNotificationQuery()
        {

        }
        public int NotificationId { get; set; }
        public DeleteSpecififNotificationQuery(int notificationId)
        {
            NotificationId = notificationId;
        }
    }
}
