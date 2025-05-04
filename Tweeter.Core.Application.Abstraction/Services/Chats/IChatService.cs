using Tweeter.Core.Application.Abstraction.Dtos.Messages;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Abstraction.Services.Chats
{
    public interface IChatService
    {
        Task<Result<MessageDto>> SendMessageAsync(string senderId, string receiverId, string content);
        Task<Result<List<MessageDto>>> GetConversationAsync(string user1Id, string user2Id);
        Task<Result> MarkMessageAsReadAsync(int messageId, string userId);
        Task<Result<int>> GetUnreadMessageCountAsync(string userId);
    }
}
