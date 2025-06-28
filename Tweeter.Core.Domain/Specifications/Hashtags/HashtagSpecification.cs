using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Specifications.Hashtags
{
    public class HashtagSpecification : BaseSpecification<Hashtag, int>
    {
        public HashtagSpecification(string? sort, int pageindex, int pagesize, string? search) : base(

                h => (string.IsNullOrEmpty(search) || h.NormalizedTagName.Contains(search.ToUpper()))


            )

        {



            switch (sort)
            {

                case "new":
                    AddOrderByDescending(x => x.CreatedOn);
                    break;

                case "Old":
                    AddOrderBy(x => x.CreatedOn);
                    break;


                default:
                    AddOrderByDescending(x => x.TweetHashtags.Count);
                    break;



            }

            ApplyPagination(pageindex, pagesize);

        }
    }
}
