namespace Tweeter.Core.Domain.AppMateData
{
	public class Router
	{
		public const string root = "api";
		public const string version = "v1";
		public const string Rule = root + "/" + version + "/";
		public static class AccountRouting
		{
			public const string prefix = Rule + "Account";

			public const string Register = prefix + "/Register";
			public const string SendCode = prefix + "/SendCode";
			public const string VerifyCode = prefix + "/VerifyCode";
			public const string EditUser = prefix + "/EditUser";

		}
		public static class AuthenticationRouting
		{
			public const string prefix = Rule + "Authentication";

			public const string Login = prefix + "/Login";
			public const string ResetPassword = prefix + "/ResetPassword";
			public const string ChangePassword = prefix + "/ChangePassword";
			public const string Logout = prefix + "/Logout";
			public const string GetCurrentUser = prefix + "/GetCurrentUser";
			public const string RefreshToken = prefix + "/RefreshToken";
			public const string RevokeRefreshToken = prefix + "/RevokeRefreshToken";

		}
		public static class ChatRouting
		{
			public const string prefix = Rule + "Chat";
			public const string messagid = "/{messageid}";

			public const string GetConversation = prefix + "/GetConversation";
			public const string SendMessage = prefix + "/SendMessage";
			public const string GetMessages = prefix + "/GetMessages";
			public const string DeleteMessage = prefix + "/DeleteMessage";
			public const string GetUnreadMessages = prefix + "/GetUnreadMessages";
			public const string MarkAsRead = prefix + "/MarkAsRead";


		}

		public static class FollowingRouting
		{
			public const string prefix = Rule + "Following";
			public const string GetCountOfFollowers = prefix + "/GetCountOfFollowers";
			public const string GetCountOfFollowing = prefix + "/GetCountOfFollowing";
			public const string GetFollowers = prefix + "/GetFollowers";
			public const string GetFollowing = prefix + "/GetFollowing";
			public const string FollowUser = prefix + "/FollowUser";
			public const string UnfollowUser = prefix + "/UnfollowUser";
			public const string IsFollowing = prefix + "/IsFollowing";
		}

		public static class NotificationRouting
		{
			public const string prefix = Rule + "Notification";
			public const string GetNotifications = prefix + "/GetNotifications";
			public const string GetCountOfUnreadable = prefix + "/GetCount-Of-Unreadable-Notifications";
			public const string MarkAsRead = prefix + "/MarkAsRead";
			public const string MarkAllAsRead = prefix + "/MarkAllAsRead";
			public const string DeleteNotification = prefix + "/DeleteNotification";
			public const string DeleteAllNotificationForSpecificUser = prefix + "/Delete-AllNotification-For-Specific-User";
			public const string GetUnreadNotifications = prefix + "/GetUnreadNotifications";
		}

		public static class CommunityRouting
		{
			public const string prefix = Rule + "Community";
			public const string CreateTweet = prefix + "/CreateTweet";
			public const string GetAllTweets = prefix + "/GetAllTweets";
			public const string GetTweetsToSpecificUser = prefix + "/GetTweetsToSpecificUser";
			public const string GetTweetsForFollowedUsers = prefix + "/GetTweetsForFollowedUsers";
			public const string GetTweetById = prefix + "/{id}";
			public const string UpdateTweet = prefix + "/Update/{id}";
			public const string DeleteTweet = prefix + "/Delete/{id}";


			public const string LikeTweet = prefix + "/Like/{id}";

			public const string Retweet = prefix + "/Retweet/{id}";
			public const string GetRetweet = prefix + "/GetRetweet/{id}";



		}

		public static class HashtagRouting
		{
			public const string prefix = Rule + "Hashtag";
			public const string GetAllHashtags = prefix + "/GetAllHashtags";
			public const string GetTweetsByHashtag = prefix + "/GetTweetsByHashtag/{hashtag}";
			public const string GetHashtagsByTweetId = prefix + "/GetHashtagsByTweetId/{tweetId}";
			public const string CreateHashtag = prefix + "/CreateHashtag";
			public const string DeleteHashtag = prefix + "/DeleteHashtag/{hashtag}";
			public const string UpdateHashtag = prefix + "/UpdateHashtag/{id}";
			public const string GetHashtagById = prefix + "/{id}";
			public const string GetTopFiveHashtages = prefix + "/Get-TopFiveHashtags";
			public const string GetTweetsByHashtagId = prefix + "/GetTweetsByHashtagId/{id}";

		}

	}
}

