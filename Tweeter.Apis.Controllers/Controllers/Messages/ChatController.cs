using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Tweeter.Apis.Controllers.Controllers.Base;
using Tweeter.Core.Application.Abstraction.Dtos.Messages;
using Tweeter.Core.Application.Features.Messages.Queries.Models;
using Tweeter.Core.Domain.AppMateData;

namespace Tweeter.Apis.Controllers.Controllers.Messages
{
    public class ChatController : BaseApiController
    {

        [Authorize]
        [HttpGet(Router.ChatRouting.GetMessages)]
        public async Task<ActionResult<MessageDto>> GetMessages([FromQuery] string userId)
        {
            var result = await mediator.Send(new GetConversationQuery() { otherUserId = userId });
            return NewResult(result);
        }
        [Authorize]
        [HttpGet(Router.ChatRouting.GetUnreadMessages)]
        public async Task<ActionResult<MessageDto>> GetUnreadMessages()
        {
            var result = await mediator.Send(new GetUnReadMessagesQuery());
            return NewResult(result);
        }
        [Authorize]
        [HttpPut(Router.ChatRouting.MarkAsRead)]
        public async Task<ActionResult<bool>> MarkAsRead([FromQuery] int messageId)
        {
            var result = await mediator.Send(new MarkMessageAsReadQuery() { MessageId = messageId });
            return NewResult(result);
        }

    }
}
