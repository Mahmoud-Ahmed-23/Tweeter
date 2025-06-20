using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Domain.Specifications.Following
{
    public class FollowersSpecification : BaseSpecification<Follow, int>
    {
        public FollowersSpecification(string? sort, string Userid, int pageSize, int pageIndex)
          : base(

                p => p.FolloweeId == Userid



                )



        {
            AddIncludes();



            switch (sort)
            {
                default:
                    AddOrderByDescending(p => p.CreatedOn);
                    break;
            }

            // totalproducts 18 ~ 20
            //page size = 5
            //page index = 3

            ApplyPagination((pageIndex - 1) * pageSize, pageSize);


        }



        public FollowersSpecification(int id) : base(id)
        {
            AddIncludes();

        }

        private protected override void AddIncludes()
        {
            base.AddIncludes();
            Includes.Add(p => p.Follower);
        }
    }
}


