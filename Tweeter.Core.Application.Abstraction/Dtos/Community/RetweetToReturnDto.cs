namespace Tweeter.Core.Application.Abstraction.Dtos.Community
{
	public class RetweetToReturnDto : TweetToReturnDto
	{
		public int? RetweetId { get; set; }
		public string? RetweetUserName { get; set; }
		public string? RetweetUserId { get; set; }
		public string? RetweetUserProfilePictureUrl { get; set; }
		public DateTime RetweetedAt { get; set; }
		public string? RetweetComment { get; set; }

		public bool IsRetweet => RetweetId > 0;

	}
}
