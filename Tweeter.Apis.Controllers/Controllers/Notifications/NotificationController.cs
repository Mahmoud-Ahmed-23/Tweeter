using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tweeter.Apis.Controllers.Controllers.Base;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Notifications;
using Tweeter.Core.Application.Features.Notifications.Commands.Models;
using Tweeter.Core.Application.Features.Notifications.Queries.Models;
using Tweeter.Core.Domain.AppMateData;

namespace Tweeter.Apis.Controllers.Controllers.Notifications
{
    [Authorize]

    public class NotificationController : BaseApiController
    {

        [HttpGet(Router.NotificationRouting.GetCountOfUnreadable)]
        public async Task<ActionResult<int>> GetCountOfUnreadable()
        {
            var result = await mediator.Send(new GetCountOfUnreadableNotificationsQuery());
            return NewResult(result);
        }

        [HttpPut(Router.NotificationRouting.MarkAllAsRead)]
        public async Task<ActionResult<bool>> MarkAllAsRead()
        {
            var result = await mediator.Send(new MarkAllAsReadCommand());
            return NewResult(result);
        }
        [HttpPut(Router.NotificationRouting.MarkAsRead)]
        public async Task<ActionResult<bool>> MarkAsRead([FromQuery] int notificationId)
        {
            var result = await mediator.Send(new MarkAsReadCommand() { NotificationId = notificationId });
            return NewResult(result);
        }
        [HttpDelete(Router.NotificationRouting.DeleteNotification)]
        public async Task<ActionResult<bool>> DeleteNotification([FromQuery] int notificationId)
        {
            var result = await mediator.Send(new DeleteSpecififNotificationQuery() { NotificationId = notificationId });
            return NewResult(result);
        }
        [HttpDelete(Router.NotificationRouting.DeleteAllNotificationForSpecificUser)]
        public async Task<ActionResult<bool>> DeleteAllNotificationForSpecificUser()
        {
            var result = await mediator.Send(new DeleteAllNotificationForSpecificUserQuery());
            return NewResult(result);
        }
        [HttpGet(Router.NotificationRouting.GetNotifications)]
        public async Task<ActionResult<Pagination<NotificationDto>>> GetNotifications([FromQuery] SpecParams specParams)
        {
            var result = await mediator.Send(new GetNotificationForUserQuery(specParams));
            return NewResult<Pagination<NotificationDto>>(result);
        }
    }
}
