using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using System.Security.Claims;
using Tweeter.Core.Application.Abstraction.Dtos.Tweets;
using Tweeter.Core.Application.Abstraction.Services.Tweets;

public class TweetHub(ITweetService _tweetService, ILogger<TweetHub> _logger) : Hub
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

			createTweetDto.UserId = userId;

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
}
