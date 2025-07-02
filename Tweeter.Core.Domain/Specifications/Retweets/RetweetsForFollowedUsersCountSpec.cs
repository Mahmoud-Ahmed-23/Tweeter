using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Specifications.Retweets
{
	public class RetweetsForFollowedUsersCountSpec : BaseSpecification<Retweet, int>
	{
		public RetweetsForFollowedUsersCountSpec(string userId)
			: base(p => p.User.Followers.Any(f => f.FollowerId == userId))
		{
		}
		private protected override void AddIncludes()
		{
			base.AddIncludes();
		}
	}
}
