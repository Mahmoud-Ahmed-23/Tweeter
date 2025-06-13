using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Tweeter.Core.Application.Abstraction.Services.Following;
using Tweeter.Core.Domain.Contracts.Persistence;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Core.Domain.Entities.Identity;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Services.Following
{
    public class FollowService(IUnitOfWork _unitOfWork, IMapper _mapper, UserManager<ApplicationUser> userManager) : IFollowService
    {
        public async Task<Result<bool>> FollowUserAsync(string followerId, string followeeId)
        {
            // Validate users
            var validationResult = await ValidateUsersAsync(followerId, followeeId);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            // Check if the follower is already following the followee
            var repo = _unitOfWork.GetRepository<Follow, int>();
            var existingFollow = await repo.GetQueryable()
                .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FolloweeId == followeeId);
            if (existingFollow != null)
            {
                return Result<bool>.Fail("You are already following this user", ErrorType.BadRequest);
            }

            // Create a new Following entity
            var following = new Follow
            {
                FollowerId = followerId,
                FolloweeId = followeeId,
            };

            // Add the new following relationship to the repository
            await repo.AddAsync(following);

            // Save changes to the database
            var completed = await _unitOfWork.CompleteAsync() > 0;
            if (!completed)
            {
                return Result<bool>.Fail("Failed to follow user", ErrorType.Unexpected);
            }

            return Result<bool>.Success(true);
        }

        public async Task<Result<bool>> UnfollowUserAsync(string followerId, string followeeId)
        {
            // Validate users
            var validationResult = await ValidateUsersAsync(followerId, followeeId);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }

            // Check if the follower is following the followee
            var repo = _unitOfWork.GetRepository<Follow, int>();
            var existingFollow = await repo.GetQueryable()
                .FirstOrDefaultAsync(f => f.FollowerId == followerId && f.FolloweeId == followeeId);
            if (existingFollow is null)
            {
                return Result<bool>.Fail("You are not following this user", ErrorType.BadRequest);
            }

            // Remove the following relationship from the repository
            repo.Delete(existingFollow);

            // Save changes to the database
            var completed = await _unitOfWork.CompleteAsync() > 0;
            if (!completed)
            {
                return Result<bool>.Fail("Failed to unfollow user", ErrorType.Unexpected);
            }

            return Result<bool>.Success(true);
        }
        private async Task<Result<bool>> ValidateUsersAsync(string followerId, string followeeId)
        {
            var follower = await userManager.FindByIdAsync(followerId);
            var followee = await userManager.FindByIdAsync(followeeId);
            if (follower is null || followee is null)
            {
                return Result<bool>.Fail("Follower or Followee not found", ErrorType.NotFound);
            }
            return Result<bool>.Success(true);
        }

        public Task<Result<int>> GetFollowerCountAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Result<int>> GetFollowingCountAsync(string userId)
        {
            throw new NotImplementedException();
        }


    }
}
