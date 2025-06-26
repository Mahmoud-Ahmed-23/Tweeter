using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Tweeter.Core.Application.Abstraction.Services.Chats;
using Tweeter.Core.Domain.Contracts.Common;
using Tweeter.Core.Domain.Contracts.Persistence;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Core.Domain.Entities.Identity;

namespace Tweeter.Core.Application.Services.Hubs
{
    [Authorize]
    public class ChatHub(IChatService chatService, ILogger<ChatHub> logger, IHubContext<NotificationHub> hubContext, IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager) : Hub
    {
        public override async Task OnConnectedAsync()
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.PrimarySid);
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, userId);
                logger.LogInformation("User {UserId} connected to chat hub", userId);

                // Notify contacts about online status
                await Clients.OthersInGroup(userId).SendAsync("UserOnline", userId);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var userId = Context.User?.FindFirstValue(ClaimTypes.PrimarySid);
            if (!string.IsNullOrEmpty(userId))
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
                logger.LogInformation("User {UserId} disconnected from chat hub", userId);

                // Notify contacts about offline status
                await Clients.OthersInGroup(userId).SendAsync("UserOffline", userId);
            }
            await base.OnDisconnectedAsync(exception);
        }
        public async Task SendMessage(string receiverId, string content)
        {
            var senderId = Context.User?.FindFirstValue(ClaimTypes.PrimarySid);
            if (string.IsNullOrEmpty(senderId))
            {
                await Clients.Caller.SendAsync("Error", "Authentication failed");
                return;
            }

            try
            {
                // Save message
                var messageDto = await chatService.SendMessageAsync(senderId, receiverId, content);

                // Send to receiver
                await Clients.User(receiverId).SendAsync("ReceiveMessage", messageDto);

                // Confirm to sender
                await Clients.Caller.SendAsync("MessageSent", messageDto);

                var notification = new Notification()
                {
                    CreatedAt = DateTime.UtcNow,
                    IsRead = false,
                    UserId = receiverId,
                    TriggerUserId = senderId,
                    NotificationType = NotificationType.Chat,
                    TweetId = null

                };


                await unitOfWork.GetRepository<Notification, int>().AddAsync(notification);

                var compelete = await unitOfWork.CompleteAsync() > 0;

                if (!compelete)
                {
                    throw new Exception("Adding Notification To DataBase Faild");
                }



                var sender = await userManager.FindByIdAsync(senderId);
                // send notification to reciver

                await hubContext.Clients.User(receiverId).SendAsync("ReceiveNotification", $"{sender!.FullName} Send Message In Chat .");
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Failed to send message from {SenderId} to {ReceiverId}", senderId, receiverId);
                await Clients.Caller.SendAsync("Error", "Failed to send message");
            }
        }

    }
}
