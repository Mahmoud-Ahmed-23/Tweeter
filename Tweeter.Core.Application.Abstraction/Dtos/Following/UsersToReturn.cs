namespace Tweeter.Core.Application.Abstraction.Dtos.Following
{
    public class UsersToReturn
    {
        public required string FullName { get; set; }
        public string? ProfilePictureUrl { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
