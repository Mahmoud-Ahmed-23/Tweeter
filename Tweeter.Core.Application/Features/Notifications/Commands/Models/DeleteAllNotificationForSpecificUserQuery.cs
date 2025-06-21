using MediatR;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Notifications.Commands.Models
{
    public class DeleteAllNotificationForSpecificUserQuery : IRequest<Response<bool>>
    {
    }
}
