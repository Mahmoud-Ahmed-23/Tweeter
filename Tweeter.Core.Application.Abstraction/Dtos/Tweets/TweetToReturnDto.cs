using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tweeter.Core.Application.Abstraction.Dtos.Tweets
{
	public class TweetToReturnDto
	{
		public int Id { get; set; }

		public string Content { get; set; }

		public string? ImageUrl { get; set; }

		public string UserName { get; set; }

		public string UserProfilePictureUrl { get; set; }

		public DateTime CreatedOn { get; set; }

		public int LikeCount { get; set; }

		public int RetweetCount { get; set; }

		public int ReplyCount { get; set; }
	}
}
