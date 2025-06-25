using AutoMapper;
using Tweeter.Core.Application.Abstraction.Dtos.Hashtags;
using Tweeter.Core.Application.Abstraction.Services.Hashtags;
using Tweeter.Core.Domain.Contracts.Persistence;
using Tweeter.Core.Domain.Entities.Data;
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
    }
}
