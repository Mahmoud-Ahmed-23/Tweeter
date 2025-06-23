using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Specifications.Tweets
{
	public class TweetsForFollowedUsersCountSpec : BaseSpecification<Tweet, int>
	{
		public TweetsForFollowedUsersCountSpec(string userId)
			: base(p => p.User.Followers.Any(f => f.FollowerId == userId))
		{
			// No additional includes or ordering needed for count specification
		}
		private protected override void AddIncludes()
		{
			base.AddIncludes();
			// No includes needed for count specification
		}
	}
}
