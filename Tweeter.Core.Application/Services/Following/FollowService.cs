using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Tweeter.Core.Application.Abstraction.Dtos.Following;
using Tweeter.Core.Application.Abstraction.Services.Following;
using Tweeter.Core.Application.Services.Hubs;
using Tweeter.Core.Domain.Contracts.Common;
using Tweeter.Core.Domain.Contracts.Persistence;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Core.Domain.Entities.Identity;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Services.Following
{
    public class FollowService(IUnitOfWork _unitOfWork
        , IMapper _mapper,
        UserManager<ApplicationUser> userManager,
        IHubContext<NotificationHub> hubContext) : IFollowService
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


            // notificate
            // You can implement notification logic here if needed
            var notification = new Notification()
            {
                CreatedAt = DateTime.UtcNow,
                IsRead = false,
                UserId = followeeId, // The user being followed
                TriggerUserId = followerId, // The user who is following
                NotificationType = NotificationType.Follow,
                TweetId = null // Assuming no tweet is associated with this follow action

            };
            await _unitOfWork.GetRepository<Notification, int>().AddAsync(notification);
            // Save the notification to the database
            var notificationCompleted = await _unitOfWork.CompleteAsync() > 0;
            if (!notificationCompleted)
            {
                return Result<bool>.Fail("Failed to create notification", ErrorType.Unexpected);
            }
            // Send notification to the followee using SignalR

            var Follower = await userManager.FindByIdAsync(followerId);

            await hubContext.Clients.User(followeeId).SendAsync("ReceiveNotification", $"{Follower!.FullName} started following you.");




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

        public async Task<Result<int>> GetFollowerCountAsync(string userId)
        {
            // Validate user
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Result<int>.Fail("User not found", ErrorType.NotFound);
            }
            // Get the count of followers
            var repo = _unitOfWork.GetRepository<Follow, int>();
            var followerCount = await repo.GetQueryable()
                .CountAsync(f => f.FolloweeId == userId);
            return Result<int>.Success(followerCount);
        }

        public async Task<Result<int>> GetFollowingCountAsync(string userId)
        {
            // Validate user
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Result<int>.Fail("User not found", ErrorType.NotFound);
            }
            // Get the count of following
            var repo = _unitOfWork.GetRepository<Follow, int>();
            var followingCount = await repo.GetQueryable()
                .CountAsync(f => f.FollowerId == userId);
            return Result<int>.Success(followingCount);
        }

        public async Task<Result<bool>> IsFollowingAsync(string followerId, string followeeId)
        {
            // Validate users
            var validationResult = await ValidateUsersAsync(followerId, followeeId);
            if (!validationResult.IsSuccess)
            {
                return validationResult;
            }
            // Check if the follower is following the followee
            var repo = _unitOfWork.GetRepository<Follow, int>();
            var isFollowing = await repo.GetQueryable()
                .AnyAsync(f => f.FollowerId == followerId && f.FolloweeId == followeeId);
            return Result<bool>.Success(isFollowing);
        }

        public async Task<Result<List<UsersToReturn>>> GetFollowersAsync(string userId)
        {
            // Validate user
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Result<List<UsersToReturn>>.Fail("User not found", ErrorType.NotFound);
            }
            // Get followers
            var repo = _unitOfWork.GetRepository<Follow, int>();
            var followers = await repo.GetQueryable()
                .Where(f => f.FolloweeId == userId)
                .Select(f => f.Follower)
                .ToListAsync();
            var followersCount = followers.Count;
            // Map to UsersToReturn DTO
            var data = _mapper.Map<List<UsersToReturn>>(followers);
            return Result<List<UsersToReturn>>.Success(data, followersCount);
        }

        public async Task<Result<List<UsersToReturn>>> GetFollowingAsync(string userId)
        {


            // Validate user
            var user = await userManager.FindByIdAsync(userId);
            if (user is null)
            {
                return Result<List<UsersToReturn>>.Fail("User not found", ErrorType.NotFound);
            }
            // Get following
            var repo = _unitOfWork.GetRepository<Follow, int>();
            var following = await repo.GetQueryable()
                .Where(f => f.FollowerId == userId)
                .Select(f => f.Followee)
                .ToListAsync();
            var followingCount = following.Count;
            // Map to UsersToReturn DTO
            var data = _mapper.Map<List<UsersToReturn>>(following);
            return Result<List<UsersToReturn>>.Success(data, followingCount);


        }

        //public async Task<Result<Pagination<UsersToReturn>>> GetFollowersAsync(string Userid, SpecParams specParams)
        //{
        //    // Validate user
        //    var user = await userManager.FindByIdAsync(Userid);
        //    if (user is null)
        //    {
        //        return Result<Pagination<UsersToReturn>>.Fail("User not found", ErrorType.NotFound);
        //    }
        //    var spec = new FollowersSpecification(specParams.Sort, specParams.Userid!, specParams.PageIndex, specParams.PageSize);
        //    var followers = await _unitOfWork.GetRepository<Follow, int>().GetAllWithSpecAsync(spec);
        //    var data = _mapper.Map<IEnumerable<UsersToReturn>>(followers);
        //    var countSpec = new FollowersCountSpecification(specParams.Userid!);
        //    var count = await _unitOfWork.GetRepository<Follow, int>().GetCountAsync(countSpec);
        //    return Result<Pagination<UsersToReturn>>.Success(new Pagination<UsersToReturn>(specParams.PageIndex, specParams.PageSize, count) { Data = data });




        //}



        //public async Task<Pagination<UsersToReturn>> GetFollowersAsync(string Userid, SpecParams specParams)
        //{
        //    // Validate user
        //    var user = await userManager.FindByIdAsync(Userid);
        //    if (user is null)
        //    {
        //        throw new ArgumentException("User not found", nameof(Userid));
        //    }

        //    var spec = new FollowersSpecification(specParams.Sort, specParams.Userid!, specParams.PageIndex,specParams.PageSize);
        //    var followers = await _unitOfWork.GetRepository<Follow, int>().GetAllWithSpecAsync(spec);
        //    var data = _mapper.Map<IEnumerable<UsersToReturn>>(followers);

        //    var countSpec = new FollowersCountSpecification(specParams.Userid!);
        //    var count = await _unitOfWork.GetRepository<Follow, int>().GetCountAsync(countSpec);
        //    return new  Pagination<UsersToReturn>(specParams.PageIndex, specParams.PageSize, count) { Data = data };
        //}


    }
}
