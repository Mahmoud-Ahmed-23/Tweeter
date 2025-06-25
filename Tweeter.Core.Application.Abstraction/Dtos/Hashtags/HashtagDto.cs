namespace Tweeter.Core.Application.Abstraction.Dtos.Hashtags
{
    public class HashtagDto
    {
        public string TagName { get; set; }


        public string NormalizedTagName { get { return TagName.ToUpper(); } }



    }
}
