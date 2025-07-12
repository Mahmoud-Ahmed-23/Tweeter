using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Specifications.Tweets
{
	public class TweetsForAllSpec : BaseSpecification<Tweet, int>
	{
		public TweetsForAllSpec(int pageIndex, int pageSize, string? search)
			: base(p => (string.IsNullOrEmpty(search) || p.NormalizedContent!.Contains(search.ToUpper())))
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
			Includes.Add(p => p.Retweets!);
			Includes.Add(p => p.Replies!);
			Includes.Add(p => p.TweetHashtags!);
			Includes.Add(p => p.Mentions!);

		}
	}
}
