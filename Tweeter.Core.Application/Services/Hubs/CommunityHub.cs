using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Application.Abstraction.Services.Community;

public class CommunityHub(ICommunityService _tweetService, ILogger<CommunityHub> _logger) : Hub
{
	public async Task SendTweet(CreateTweetDto createTweetDto)
	{
		try
		{
			var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(userId))
			{
				await Clients.Caller.SendAsync("Error", "Unauthorized");
				return;
			}

			var tweetDto = await _tweetService.CreateTweetAsync(createTweetDto);

			if (tweetDto.IsSuccess)
			{
				await Clients.All.SendAsync("NewTweet", tweetDto.Data);
			}
			else
			{
				await Clients.Caller.SendAsync("Error", tweetDto.ErrorMessage);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error sending tweet");
			await Clients.Caller.SendAsync("Error", "Failed to send tweet");
		}
	}

	public async Task UpdateTweet(int tweetId, UpdateTweetDto dto)
	{
		try
		{
			var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(userId))
			{
				await Clients.Caller.SendAsync("Error", "Unauthorized");

				return;
			}

			var result = await _tweetService.UpdateTweetAsync(tweetId, dto);

			if (result.IsSuccess)
			{
				await Clients.All.SendAsync("TweetUpdated", result.Data);
			}
			else
			{
				await Clients.Caller.SendAsync("Error", result.ErrorMessage);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error updating tweet");

			await Clients.Caller.SendAsync("Error", "Failed to update tweet");
		}
	}

	public async Task DeleteTweet(int tweetId)
	{
		try
		{
			var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(userId))
			{
				await Clients.Caller.SendAsync("Error", "Unauthorized");

				return;
			}

			var result = await _tweetService.DeleteTweetAsync(tweetId);

			if (result.IsSuccess)
			{
				await Clients.All.SendAsync("TweetDeleted", tweetId);
			}
			else
			{
				await Clients.Caller.SendAsync("Error", result.ErrorMessage);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error deleting tweet");

			await Clients.Caller.SendAsync("Error", "Failed to delete tweet");
		}
	}

	public async Task LikeTweet(int tweetId)
	{
		try
		{
			var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(userId))
			{
				await Clients.Caller.SendAsync("Error", "Unauthorized");
				return;
			}

			var result = await _tweetService.LikeTweetAsync(tweetId);

			if (result.IsSuccess)
			{
				// Broadcast the updated like count or message
				await Clients.All.SendAsync("TweetLiked", new { TweetId = tweetId, Message = result.Data });
			}
			else
			{
				await Clients.Caller.SendAsync("Error", result.ErrorMessage);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error liking tweet");
			await Clients.Caller.SendAsync("Error", "Failed to like tweet");
		}
	}

	public async Task Retweet(int tweetId, string? comment)
	{
		try
		{
			var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);

			if (string.IsNullOrEmpty(userId))
			{
				await Clients.Caller.SendAsync("Error", "Unauthorized");
				return;
			}

			var result = await _tweetService.RetweetAsync(tweetId, comment);

			if (result.IsSuccess)
			{
				await Clients.All.SendAsync("NewRetweet", result.Data);
			}
			else
			{
				await Clients.Caller.SendAsync("Error", result.ErrorMessage);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error retweeting");
			await Clients.Caller.SendAsync("Error", "Failed to retweet");
		}
	}

	public async Task UnRetweet(int retweetId)
	{
		try
		{
			var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userId))
			{
				await Clients.Caller.SendAsync("Error", "Unauthorized");
				return;
			}

			var result = await _tweetService.UnRetweetAsync(retweetId);

			if (result.IsSuccess)
			{
				await Clients.All.SendAsync("RetweetDeleted", retweetId);
			}
			else
			{
				await Clients.Caller.SendAsync("Error", result.ErrorMessage);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error unretweeting");
			await Clients.Caller.SendAsync("Error", "Failed to unretweet");
		}
	}

	public async Task LikeRetweet(int retweetId)
	{
		try
		{
			var userId = Context.User?.FindFirstValue(ClaimTypes.NameIdentifier);
			if (string.IsNullOrEmpty(userId))
			{
				await Clients.Caller.SendAsync("Error", "Unauthorized");
				return;
			}

			var result = await _tweetService.LikeRetweetAsync(retweetId);

			if (result.IsSuccess)
			{
				await Clients.All.SendAsync("RetweetLiked", new { RetweetId = retweetId, Message = result.Data });
			}
			else
			{
				await Clients.Caller.SendAsync("Error", result.ErrorMessage);
			}
		}
		catch (Exception ex)
		{
			_logger.LogError(ex, "Error liking retweet");
			await Clients.Caller.SendAsync("Error", "Failed to like retweet");
		}
	}


	public override async Task OnConnectedAsync()
	{
		var userId = Context.User?.FindFirstValue(ClaimTypes.PrimarySid);
		if (!string.IsNullOrEmpty(userId))
		{
			await Groups.AddToGroupAsync(Context.ConnectionId, userId);
			_logger.LogInformation("User {UserId} connected to tweet hub", userId);

			await Clients.All.SendAsync("UserOnline", userId);
		}

		await base.OnConnectedAsync();
	}

	public override async Task OnDisconnectedAsync(Exception? exception)
	{
		var userId = Context.User?.FindFirstValue(ClaimTypes.PrimarySid);
		if (!string.IsNullOrEmpty(userId))
		{
			await Groups.RemoveFromGroupAsync(Context.ConnectionId, userId);
			_logger.LogInformation("User {UserId} disconnected from tweet hub", userId);

			await Clients.All.SendAsync("UserOffline", userId);
		}

		await base.OnDisconnectedAsync(exception);
	}
}
