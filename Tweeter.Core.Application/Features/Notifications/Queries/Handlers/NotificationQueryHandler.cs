using MediatR;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Notifications;
using Tweeter.Core.Application.Abstraction.Services.Notifications;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Application.Features.Notifications.Queries.Models;

namespace Tweeter.Core.Application.Features.Notifications.Queries.Handlers
{
    public class NotificationQueryHandler : BaseHandler,
        IRequestHandler<GetCountOfUnreadableNotificationsQuery, Response<int>>,
        IRequestHandler<GetNotificationForUserQuery, Response<Pagination<NotificationDto>>>

    {
        private readonly INotificationService _notificationService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public NotificationQueryHandler(INotificationService notificationService, IHttpContextAccessor httpContextAccessor)
        {
            _notificationService = notificationService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Response<int>> Handle(GetCountOfUnreadableNotificationsQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);
            if (string.IsNullOrEmpty(userId))
            {
                return Fail<int>("User ID cannot be null or empty.");
            }
            var result = await _notificationService.CountOfUnReadNotificationAsync(userId);

            return await HandleResultAsync(Task.FromResult(result));


        }

        public async Task<Response<Pagination<NotificationDto>>> Handle(GetNotificationForUserQuery request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.PrimarySid);
            if (string.IsNullOrEmpty(userId))
            {
                return Fail<Pagination<NotificationDto>>("User ID cannot be null or empty.");
            }
            var result = await _notificationService.GetNotificationsAsync(userId, request.SpecParams);
            return await HandleResultAsync(Task.FromResult(result));
        }
    }
}
