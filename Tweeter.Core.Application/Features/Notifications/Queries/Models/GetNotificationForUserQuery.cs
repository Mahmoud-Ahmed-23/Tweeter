using MediatR;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Notifications;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Notifications.Queries.Models
{
    public class GetNotificationForUserQuery : IRequest<Response<Pagination<NotificationDto>>>
    {
        public SpecParams SpecParams { get; set; }
        public GetNotificationForUserQuery(SpecParams specParams)
        {
            SpecParams = specParams;
        }
    }

}
