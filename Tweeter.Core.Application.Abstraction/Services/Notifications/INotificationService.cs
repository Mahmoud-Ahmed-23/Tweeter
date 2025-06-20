using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Abstraction.Services.Notifications
{
    public interface INotificationService
    {
        //Task<IEnumerable<NotificationDto>> GetNotificationsAsync(string userId);
        //Task<Result<IEnumerable<NotificationDto>>> GetUnreadNotificationsAsync(string userId);
        Task<Result<bool>> MarkAsReadAsync(int notificationId);
        Task<Result<bool>> MarkAllAsReadAsync(string userId);
        Task<Result<bool>> DeleteNotificationAsync(int notificationId);
        Task<Result<bool>> DeleteAllNotificationsForSpecificUserAsync(string userId);

        Task<Result<int>> CountOfUnReadNotificationAsync(string userid);


    }
}
