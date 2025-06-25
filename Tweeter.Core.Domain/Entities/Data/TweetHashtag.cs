namespace Tweeter.Core.Domain.Entities.Data
{
    public class TweetHashtag
    {
        public int HashtagId { get; set; }

        public int TweetId { get; set; }

        // Navigation properties
        public virtual Tweet Tweet { get; set; }
        public virtual Hashtag Hashtag { get; set; }
    }
}
