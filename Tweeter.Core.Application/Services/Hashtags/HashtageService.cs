using AutoMapper;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Hashtags;
using Tweeter.Core.Application.Abstraction.Dtos.Tweets;
using Tweeter.Core.Application.Abstraction.Services.Hashtags;
using Tweeter.Core.Domain.Contracts.Persistence;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Core.Domain.Specifications.Hashtags;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Services.Hashtags
{
    public class HashtageService(IUnitOfWork unitOfWork, IMapper mapper) : IHashtageService
    {
        public async Task<Result<bool>> CreateAsync(HashtagDto hashtag)
        {

            var hashtagrepo = unitOfWork.GetRepository<Hashtag, int>();


            var entity = mapper.Map<Hashtag>(hashtag);
            await hashtagrepo.AddAsync(entity);
            var result = await unitOfWork.CompleteAsync() > 0;
            if (!result)
            {
                return Result<bool>.Fail("Failed to create hashtag", ErrorType.BadRequest);
            }
            return Result<bool>.Success(result, 1);
        }



        public async Task<Result<HashtagToReturn>> UpdateAsync(int id, HashtagDto hashtag)
        {



            var hashtagrepo = unitOfWork.GetRepository<Hashtag, int>();
            var entity = await hashtagrepo.GetAsync(id);
            if (entity == null)
            {
                return Result<HashtagToReturn>.Fail("Hashtag not found", ErrorType.NotFound);
            }
            mapper.Map(hashtag, entity);
            hashtagrepo.Update(entity);
            var result = await unitOfWork.CompleteAsync() > 0;
            if (!result)
            {
                return Result<HashtagToReturn>.Fail("Failed to update hashtag", ErrorType.BadRequest);
            }
            var mappeddata = mapper.Map<HashtagToReturn>(entity);




            return Result<HashtagToReturn>.Success(mappeddata, 1);
        }


        public async Task<Result<HashtagToReturn>> GetByIdAsync(int id)
        {
            var hashtagrepo = unitOfWork.GetRepository<Hashtag, int>();
            var entity = await hashtagrepo.GetAsync(id);
            if (entity is null)
            {
                return Result<HashtagToReturn>.Fail("Hashtag not found", ErrorType.NotFound);
            }
            var mappeddata = mapper.Map<HashtagToReturn>(entity);
            return Result<HashtagToReturn>.Success(mappeddata, 1);
        }

        public async Task<Result<bool>> DeleteAsync(int id)
        {

            var hashtagrepo = unitOfWork.GetRepository<Hashtag, int>();
            var entity = await hashtagrepo.GetAsync(id);
            if (entity is null)
            {
                return Result<bool>.Fail("Hashtag not found", ErrorType.NotFound);
            }
            hashtagrepo.Delete(entity);
            var result = await unitOfWork.CompleteAsync() > 0;
            if (!result)
            {
                return Result<bool>.Fail("Failed to delete hashtag", ErrorType.BadRequest);
            }
            return Result<bool>.Success(result, 1);
        }

        public async Task<Result<IEnumerable<HashtagToReturn>>> GetTopFiveHashtagsBasedOnCountOfTweetsAsync()
        {
            var hashtagrepo = unitOfWork.GetRepository<Hashtag, int>();
            var entities = await hashtagrepo.GetAllAsync();
            if (entities is null || !entities.Any())
            {
                return Result<IEnumerable<HashtagToReturn>>.Fail("No hashtags found", ErrorType.NotFound);
            }
            var sortedHashtags = entities.OrderByDescending(h => h.TweetHashtags.Count).Take(5);
            var mappedData = mapper.Map<IEnumerable<HashtagToReturn>>(sortedHashtags);
            foreach (var hashtag in mappedData)
            {
                hashtag.TweetCount = sortedHashtags.FirstOrDefault(h => h.Id == hashtag.Id)?.TweetHashtags.Count ?? 0;
            }
            return Result<IEnumerable<HashtagToReturn>>.Success(mappedData, mappedData.Count());
        }



        public async Task<Result<Pagination<TweetToReturnDto>>> GetTweetsByHashtagIdAsync(int id, SpecParams specParams)
        {


            //with specification
            var spec = new HashtagTweetsSpecification(id, specParams.Sort, specParams.PageIndex, specParams.PageSize);
            var tweethashtagrepo = unitOfWork.GetRepository<TweetHashtag, int>();
            var tweets = await tweethashtagrepo.GetAllWithSpecAsync(spec);
            if (tweets is null || !tweets.Any())
            {
                return Result<Pagination<TweetToReturnDto>>.Fail("No tweets found for this hashtag", ErrorType.NotFound);
            }
            var mappedTweets = mapper.Map<IEnumerable<TweetToReturnDto>>(tweets.Select(th => th.Tweet));

            var totalCount = tweets.Count();

            return Result<Pagination<TweetToReturnDto>>.Success(new Pagination<TweetToReturnDto>(specParams.PageIndex, specParams.PageSize, totalCount) { Data = mappedTweets });




            //var hashtagrepo = unitOfWork.GetRepository<Hashtag, int>();
            //var entity = await hashtagrepo.GetAsync(id);
            //if (entity is null)
            //{
            //    return Result<IEnumerable<TweetToReturnDto>>.Fail("Hashtag not found", ErrorType.NotFound);
            //}

            //// include tweets associated with the hashtag
            //// using SelectMany to flatten the collection of tweets associated with the hashtag

            //var tweets = hashtagrepo.GetQueryable()
            //    .Where(h => h.Id == id)
            //    .SelectMany(h => h.TweetHashtags.Select(th => th.Tweet))
            //    .ToList();
            //if (tweets is null || !tweets.Any())
            //{
            //    return Result<IEnumerable<TweetToReturnDto>>.Fail("No tweets found for this hashtag", ErrorType.NotFound);
            //}
            //var mappedTweets = mapper.Map<IEnumerable<TweetToReturnDto>>(tweets);
            //return Result<IEnumerable<TweetToReturnDto>>.Success(mappedTweets, mappedTweets.Count());



        }
    }
}
