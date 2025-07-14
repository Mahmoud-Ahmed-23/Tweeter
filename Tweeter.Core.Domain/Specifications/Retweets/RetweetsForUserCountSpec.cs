using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Specifications.Retweets
{
	public class RetweetsForUserCountSpec : BaseSpecification<Retweet, int>
	{
		public RetweetsForUserCountSpec(string userId)
			: base(p => p.UserId == userId)
		{
		}


	}
}
