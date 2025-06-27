using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Hashtags;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Abstraction.Services.Hashtags
{
    public interface IHashtageService
    {

        //Task<Pagination<HashtagToReturn>> GetAllAsync(SpecParams specParams);


        Task<Result<Pagination<TweetToReturnDto>>> GetTweetsByHashtagIdAsync(int id, SpecParams specParams);



        Task<Result<bool>> CreateAsync(HashtagDto hashtag);
        Task<Result<HashtagToReturn>> UpdateAsync(int id, HashtagDto hashtag);

        Task<Result<HashtagToReturn>> GetByIdAsync(int id);
        Task<Result<bool>> DeleteAsync(int id);
        Task<Result<IEnumerable<HashtagToReturn>>> GetTopFiveHashtagsBasedOnCountOfTweetsAsync();


    }
}
