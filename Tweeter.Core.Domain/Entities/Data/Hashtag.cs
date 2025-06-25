using Tweeter.Core.Domain.Common;

namespace Tweeter.Core.Domain.Entities.Data
{
    public class Hashtag : BaseAuditableEntity<int>
    {
        public string TagName { get; set; }




        // Navigation properties
        public virtual ICollection<TweetHashtag> TweetHashtags { get; set; }
    }
}
