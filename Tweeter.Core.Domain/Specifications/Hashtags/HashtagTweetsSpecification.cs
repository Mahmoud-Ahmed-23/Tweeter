using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Specifications.Hashtags
{
    public class HashtagTweetsSpecification : BaseSpecification<TweetHashtag, int>
    {
        public HashtagTweetsSpecification(int id, string? sort, int pageindex, int pagesize) : base(

            p => p.HashtagId == id


            )
        {



            ApplyPagination(pageindex, pagesize);


            switch (sort)
            {
                case "New":
                    AddOrderByDescending(p => p.Tweet!.CreatedOn);
                    break;
                case "Od":
                    AddOrderBy(p => p.Tweet!.CreatedOn);
                    break;
                default:
                    AddOrderByDescending(p => p.Tweet!.CreatedOn);
                    break;
            }





            AddIncludes();

        }

        private protected override void AddIncludes()
        {
            Includes.Add(p => p.Tweet);
            Includes.Add(p => p.Hashtag);
        }


    }
}
