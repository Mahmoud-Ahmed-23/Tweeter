using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tweeter.Core.Application.Abstraction.Dtos.Messages;
using Tweeter.Core.Application.Abstraction.Services.Chats;
using Tweeter.Core.Domain.Contracts.Persistence;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Core.Domain.Entities.Identity;
using Tweeter.Infrastructure.Persistence._Data;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Services.Chats
{
    public class ChatService(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager, TweeterDbContext tweeterDbContext) : IChatService
    {
        public async Task<Result<List<MessageDto>>> GetConversationAsync(string user1Id, string user2Id)
        {



            var messages = await tweeterDbContext.Messages
                        .Where(m => (m.SenderId == user1Id && m.ReceiverId == user2Id) ||
                                   (m.SenderId == user2Id && m.ReceiverId == user1Id))
                        .OrderBy(m => m.SentAt)
                        .Select(m => new MessageDto
                        {
                            Id = m.Id,
                            Content = m.Content,
                            SenderId = m.SenderId,
                            SenderUsername = m.Sender.FullName!,
                            ReceiverId = m.ReceiverId,
                            SentAt = m.SentAt,
                            IsRead = m.IsRead
                        })
                        .ToListAsync();
            if (messages == null || messages.Count == 0)
            {
                return Result<List<MessageDto>>.Fail("No messages found", ErrorType.NotFound); // Return empty result
            }

            return Result<List<MessageDto>>.Success(messages); // Wrap in Result

        }

        public async Task<Result<int>> GetUnreadMessageCountAsync(string userId)
        {
            var repo = unitOfWork.GetRepository<Message, int>();
            var unreadCount = await repo.GetQueryable().CountAsync(m => m.ReceiverId == userId && !m.IsRead);
            if (unreadCount == 0)
            {
                return Result<int>.Fail("No unread messages found", ErrorType.NotFound);
            }

            return Result<int>.Success(unreadCount);


        }

        public async Task<Result> MarkMessageAsReadAsync(int messageId, string userId)
        {


            var message = await unitOfWork.GetRepository<Message, int>()
                .GetQueryable()
                .FirstOrDefaultAsync(m => m.Id == messageId && m.ReceiverId == userId);
            if (message == null)
            {
                return Result.Fail("Message not found", ErrorType.NotFound);
            }
            if (message.IsRead)
            {
                return Result.Fail("Message already marked as read", ErrorType.BadRequest);
            }
            message.IsRead = true;
            var completed = await unitOfWork.CompleteAsync() > 0;
            if (!completed)
            {
                return Result.Fail("Failed to mark message as read", ErrorType.Unexpected);
            }
            return Result.Success();


        }

        public async Task<Result<MessageDto>> SendMessageAsync(string senderId, string receiverId, string content)
        {
            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                Content = content,
                IsRead = false
            };
            var repo = unitOfWork.GetRepository<Message, int>();
            await repo.AddAsync(message);
            var completed = await unitOfWork.CompleteAsync() > 0;
            if (!completed)
            {
                return Result<MessageDto>.Fail("Failed to send message", ErrorType.Unexpected);
            }

            var sender = await userManager.Users.FirstOrDefaultAsync(u => u.Id == senderId);
            if (sender == null)
            {
                return Result<MessageDto>.Fail("Sender not found", ErrorType.NotFound);
            }

            return Result<MessageDto>.Success(new MessageDto
            {
                Id = message.Id,
                Content = message.Content,
                SenderId = senderId,
                SenderUsername = sender.FullName!,
                ReceiverId = receiverId,
                SentAt = message.SentAt,
                IsRead = message.IsRead
            });


        }

    }

}
