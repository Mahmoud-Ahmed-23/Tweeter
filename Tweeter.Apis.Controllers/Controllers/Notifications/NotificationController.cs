using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tweeter.Apis.Controllers.Controllers.Base;
using Tweeter.Core.Application.Features.Notifications.Queries.Models;
using Tweeter.Core.Domain.AppMateData;

namespace Tweeter.Apis.Controllers.Controllers.Notifications
{

    public class NotificationController : BaseApiController
    {

        [Authorize]
        [HttpGet(Router.NotificationRouting.GetCountOfUnreadable)]
        public async Task<ActionResult<int>> GetCountOfUnreadable()
        {
            var result = await mediator.Send(new GetCountOfUnreadableNotificationsQuery());
            return NewResult(result);
        }

    }
}
