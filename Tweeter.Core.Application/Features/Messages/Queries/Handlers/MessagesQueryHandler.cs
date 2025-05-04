using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Tweeter.Core.Application.Abstraction.Dtos.Messages;
using Tweeter.Core.Application.Abstraction.Services.Chats;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Messages.Queries.Models;

namespace Tweeter.Core.Application.Features.Messages.Queries.Handlers
{
    public class MessagesQueryHandler : BaseHandler,
        IRequestHandler<GetConversationQuery, Response<List<MessageDto>>>,
        IRequestHandler<GetUnReadMessagesQuery, Response<int>>,
        IRequestHandler<MarkMessageAsReadQuery, Response<bool>>
    {
        private readonly IChatService _chatService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public MessagesQueryHandler(IChatService chatService, IHttpContextAccessor httpContextAccessor)
        {
            _chatService = chatService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<Response<List<MessageDto>>> Handle(GetConversationQuery request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;

            var userId = user?.FindFirst(ClaimTypes.PrimarySid)?.Value;


            var result = await _chatService.GetConversationAsync(userId!, request.otherUserId);
            return await HandleResultAsync(Task.FromResult(result));

        }

        public async Task<Response<int>> Handle(GetUnReadMessagesQuery request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var userId = user?.FindFirst(ClaimTypes.PrimarySid)?.Value;
            var result = await _chatService.GetUnreadMessageCountAsync(userId!);
            return await HandleResultAsync(Task.FromResult(result));

        }

        public async Task<Response<bool>> Handle(MarkMessageAsReadQuery request, CancellationToken cancellationToken)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            var userId = user?.FindFirst(ClaimTypes.PrimarySid)?.Value;
            var result = await _chatService.MarkMessageAsReadAsync(request.MessageId, userId!);
            return await HandleResultAsync(Task.FromResult(result));

        }
    }

}
