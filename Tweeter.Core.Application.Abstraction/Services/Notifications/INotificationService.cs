using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Notifications;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Abstraction.Services.Notifications
{
    public interface INotificationService
    {
        Task<Result<Pagination<NotificationDto>>> GetNotificationsAsync(string userId, SpecParams specParams);
        Task<Result<Pagination<NotificationDto>>> GetUnreadNotificationsAsync(string userId, SpecParams specParams);
        Task<Result<bool>> MarkAsReadAsync(int notificationId);
        Task<Result<bool>> MarkAllAsReadAsync(string userId);
        Task<Result<bool>> DeleteNotificationAsync(int notificationId);
        Task<Result<bool>> DeleteAllNotificationsForSpecificUserAsync(string userId);

        Task<Result<int>> CountOfUnReadNotificationAsync(string userid);


    }
}
