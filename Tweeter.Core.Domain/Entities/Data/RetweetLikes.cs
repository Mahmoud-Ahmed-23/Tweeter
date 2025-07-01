using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Domain.Common;
using Tweeter.Core.Domain.Entities.Identity;

namespace Tweeter.Core.Domain.Entities.Data
{
	public class RetweetLikes : BaseAuditableEntity<int>
	{
		public string UserId { get; set; }
		public int RetweetId { get; set; }

		// Navigation properties
		public virtual ApplicationUser User { get; set; }
		public virtual Retweet Retweet { get; set; }
	}
}
