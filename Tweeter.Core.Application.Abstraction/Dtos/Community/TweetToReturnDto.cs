using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Core.Application.Abstraction.Dtos.Community
{
	public class TweetToReturnDto
	{
		public int TweetId { get; set; }

		public string TweetContent { get; set; }

		public string? TweetImageUrl { get; set; }

		public string TweetUserName { get; set; }

		public string TweetUserId { get; set; }

		public string TweetUserProfilePictureUrl { get; set; }

		public DateTime CreatedOn { get; set; }

		public int RetweetLikeCount { get; set; }

		public int RetweetCount { get; set; }

		public int RetweetReplyCount { get; set; }
	}
}
