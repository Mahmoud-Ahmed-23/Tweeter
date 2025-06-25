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
    }
}
