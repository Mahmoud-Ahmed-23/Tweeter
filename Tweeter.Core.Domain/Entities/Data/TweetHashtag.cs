using Tweeter.Core.Domain.Common;

namespace Tweeter.Core.Domain.Entities.Data
{
    public class TweetHashtag : BaseEntity<int>
    {
        public int HashtagId { get; set; }

        // Navigation properties
        public virtual Tweet Tweet { get; set; }
        public virtual Hashtag Hashtag { get; set; }
    }
}
