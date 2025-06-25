namespace Tweeter.Core.Application.Abstraction.Dtos.Hashtags
{
    public class HashtagToReturn
    {
        public int Id { get; set; }
        public string TagName { get; set; } = string.Empty;
        public string NormalizedTagName { get; set; } = string.Empty;
        public string CreatedBy { get; set; } = null!;

        public DateTime CreatedOn { get; set; }

        public string LastModifiedBy { get; set; } = null!;


        public DateTime LastModifiedOn { get; set; }

        public int? TweetCount { get; set; }
    }
}
