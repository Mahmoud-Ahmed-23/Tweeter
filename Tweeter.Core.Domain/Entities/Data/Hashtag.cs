using Tweeter.Core.Domain.Common;

namespace Tweeter.Core.Domain.Entities.Data
{
    public class Hashtag : BaseEntity<int>
    {
        public string Tag { get; set; }

        // Navigation properties
        public virtual ICollection<TweetHashtag> TweetHashtags { get; set; }
    }
}
