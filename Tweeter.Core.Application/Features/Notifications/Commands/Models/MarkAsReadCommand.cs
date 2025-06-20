using MediatR;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Notifications.Commands.Models
{
    public class MarkAsReadCommand : IRequest<Response<bool>>
    {
        public int NotificationId { get; set; }
        public MarkAsReadCommand()
        {

        }
        public MarkAsReadCommand(int notificationId)
        {

            NotificationId = notificationId;
        }
    }
}
