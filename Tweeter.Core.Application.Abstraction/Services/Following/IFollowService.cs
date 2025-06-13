using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Abstraction.Services.Following
{
    public interface IFollowService
    {


        Task<Result<bool>> FollowUserAsync(string followerId, string followeeId);


        Task<Result<bool>> UnfollowUserAsync(string followerId, string followeeId);
        //Task<IEnumerable<FollwersToReturn>> GetFollowersAsync(string userId);
        //Task<List<string>> GetFollowingAsync(string userId);
        Task<Result<int>> GetFollowerCountAsync(string userId);
        Task<Result<int>> GetFollowingCountAsync(string userId);
        Task<Result<bool>> IsFollowingAsync(string followerId, string followeeId);

    }
}
