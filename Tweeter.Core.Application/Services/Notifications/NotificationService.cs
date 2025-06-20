using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Tweeter.Core.Application.Abstraction.Services.Notifications;
using Tweeter.Core.Domain.Contracts.Persistence;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Core.Domain.Entities.Identity;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Services.Notifications
{
    public class NotificationService(IUnitOfWork unitOfWork, IMapper mapper, UserManager<ApplicationUser> userManager) : INotificationService
    {
        public async Task<Result<int>> CountOfUnReadNotificationAsync(string userid)
        {

            if (string.IsNullOrEmpty(userid))
            {
                return Result<int>.Fail("User ID cannot be null or empty.");
            }
            var user = await userManager.FindByIdAsync(userid);
            if (user == null)
            {
                return Result<int>.Fail("User not found.");
            }

            var Notificationrepo = unitOfWork.GetRepository<Notification, int>();

            var unreadcount = Notificationrepo.GetQueryable()
                .Where(n => n.UserId == userid && !n.IsRead)
                .Count();
            return Result<int>.Success(unreadcount);




        }

        public Task<Result<bool>> DeleteAllNotificationsForSpecificUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<bool>> DeleteNotificationAsync(int notificationId)
        {
            throw new NotImplementedException();
        }

        public async Task<Result<bool>> MarkAllAsReadAsync(string userId)
        {
            if (string.IsNullOrEmpty(userId))
            {
                return Result<bool>.Fail("User ID cannot be null or empty.");
            }
            var user = await userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return Result<bool>.Fail("User not found.");
            }
            var notificationRepo = unitOfWork.GetRepository<Notification, int>();
            var notifications = notificationRepo.GetQueryable()
                .Where(n => n.UserId == userId && !n.IsRead);
            foreach (var notification in notifications)
            {
                notification.IsRead = true;
            }
            var compelete = await unitOfWork.CompleteAsync() > 0;
            if (!compelete)
            {
                return Result<bool>.Fail("Failed to mark all notifications as read.");
            }
            return Result<bool>.Success(true);

        }

        public Task<Result<bool>> MarkAsReadAsync(int notificationId)
        {
            throw new NotImplementedException();
        }
    }
}
