namespace Tweeter.Core.Application.Abstraction.Dtos.Community
{
	public class RetweetToReturnDto : TweetToReturnDto
	{
		public int RetweetId { get; set; }
		public required string RetweetUserName { get; set; }
		public required string RetweetUserProfilePictureUrl { get; set; }
		public DateTime RetweetedAt { get; set; }
		public string? Comment { get; set; }

	}
}
