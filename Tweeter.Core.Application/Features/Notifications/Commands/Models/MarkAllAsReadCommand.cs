using MediatR;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Notifications.Commands.Models
{
    public class MarkAllAsReadCommand : IRequest<Response<bool>>
    {
    }
}
