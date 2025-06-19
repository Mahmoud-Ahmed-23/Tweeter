using Tweeter.Core.Application.Abstraction.Dtos.Following;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Abstraction.Services.Following
{
    public interface IFollowService
    {


        Task<Result<bool>> FollowUserAsync(string followerId, string followeeId);


        Task<Result<bool>> UnfollowUserAsync(string followerId, string followeeId);

        Task<Result<List<UsersToReturn>>> GetFollowersAsync(string userId);
        Task<Result<List<UsersToReturn>>> GetFollowingAsync(string userId);



        Task<Result<int>> GetFollowerCountAsync(string userId);
        Task<Result<int>> GetFollowingCountAsync(string userId);
        Task<Result<bool>> IsFollowingAsync(string followerId, string followeeId);

    }
}
