namespace Tweeter.Core.Application.Abstraction.Dtos.Messages
{
    public class MessageDto
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public string SenderId { get; set; }
        public string SenderUsername { get; set; }
        //public string SenderProfileImage { get; set; }
        public string ReceiverId { get; set; }
        public DateTime SentAt { get; set; }
        public bool IsRead { get; set; }
    }
}
