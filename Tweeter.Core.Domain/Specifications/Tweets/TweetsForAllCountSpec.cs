using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Specifications.Tweets
{
	public class TweetsForAllCountSpec : BaseSpecification<Tweet, int>
	{
		public TweetsForAllCountSpec() { }
	}
}
