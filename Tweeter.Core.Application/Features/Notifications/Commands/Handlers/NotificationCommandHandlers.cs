using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Tweeter.Core.Application.Abstraction.Services.Notifications;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Notifications.Commands.Models;

namespace Tweeter.Core.Application.Features.Notifications.Commands.Handlers
{
    public class NotificationCommandHandlers : BaseHandler, IRequestHandler<MarkAllAsReadCommand, Response<bool>>
    {
        private readonly INotificationService _notificationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationCommandHandlers(INotificationService notificationService, IHttpContextAccessor httpContextAccessor)
        {
            _notificationService = notificationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<bool>> Handle(MarkAllAsReadCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);
            if (string.IsNullOrEmpty(userId))
            {
                return Fail<bool>("User ID cannot be null or empty.");
            }
            var result = await _notificationService.MarkAllAsReadAsync(userId);
            return await HandleResultAsync(Task.FromResult(result));
        }
    }
}
