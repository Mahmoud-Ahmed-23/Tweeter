using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Following;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Abstraction.Services.Following
{
    public interface IFollowService
    {


        Task<Result<bool>> FollowUserAsync(string followerId, string followeeId);


        Task<Result<bool>> UnfollowUserAsync(string followerId, string followeeId);
        Task<IEnumerable<Pagination<UsersToReturn>>> GetFollowersAsync(SpecParams specParams);
        Task<IEnumerable<Pagination<UsersToReturn>>> GetFollowingAsync(SpecParams specParams);
        Task<Result<int>> GetFollowerCountAsync(string userId);
        Task<Result<int>> GetFollowingCountAsync(string userId);
        Task<Result<bool>> IsFollowingAsync(string followerId, string followeeId);

    }
}
