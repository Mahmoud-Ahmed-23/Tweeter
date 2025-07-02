using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Specifications.Retweets
{
	public class RetweetsForFollowedUsersSpec : BaseSpecification<Retweet, int>
	{
		public RetweetsForFollowedUsersSpec(string userId, int pageIndex, int pageSize)
			: base(p => p.User.Followers.Any(p => p.FollowerId == userId))
		{
			AddIncludes();
			AddOrderByDescending(p => p.CreatedOn);
			ApplyPagination((pageIndex - 1) * pageSize, pageSize);
		}

		private protected override void AddIncludes()
		{
			base.AddIncludes();
			Includes.Add(p => p.User!);
			Includes.Add(p => p.Likes!);
			Includes.Add(p => p.Replies!);
			Includes.Add(p => p.TweetHashtags!);
			Includes.Add(p => p.Mentions!);
			Includes.Add(p => p.OriginalTweet!);
		}
	}
}
