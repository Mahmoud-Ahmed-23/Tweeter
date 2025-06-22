using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Specifications.Tweets
{
	public class TweetsForUserCountSpec : BaseSpecification<Tweet, int>
	{
		public TweetsForUserCountSpec(string userid)
			: base(p => p.UserId == userid)
		{
		}

	}
}
