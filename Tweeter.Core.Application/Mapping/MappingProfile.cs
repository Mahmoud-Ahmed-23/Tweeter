using AutoMapper;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Application.Abstraction.Dtos.Following;
using Tweeter.Core.Application.Abstraction.Dtos.Hashtags;
using Tweeter.Core.Application.Abstraction.Dtos.Notifications;
using Tweeter.Core.Application.Mapping.NotificationResolvers;
using Tweeter.Core.Application.Mapping.RetweetResolvers;
using Tweeter.Core.Application.Mapping.TweetResolvers;
using Tweeter.Core.Domain.Entities.Data;
using Tweeter.Core.Domain.Entities.Identity;

namespace Tweeter.Core.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            // Add your mapping configurations here
            // For example:
            // CreateMap<SourceEntity, DestinationEntity>();
            // CreateMap<SourceDto, DestinationDto>();
            // Example for a specific mapping

            CreateMap<ApplicationUser, UsersToReturn>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => src.FullName))
                .ForMember(dest => dest.ProfilePictureUrl, opt => opt.MapFrom(src => src.ProfilePictureUrl))
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt)).ReverseMap();

			CreateMap<CreateTweetDto, Tweet>();
			CreateMap<Tweet, TweetToReturnDto>()
				.ForMember(dest => dest.TweetId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
				.ForMember(dest => dest.UserProfilePictureUrl, opt => opt.MapFrom<TweetUserProfilePictureUrlResolver>())
				.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<TweetPictureUrlResolver>())
				.ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => src.Likes.Count))
				.ForMember(dest => dest.RetweetCount, opt => opt.MapFrom(src => src.Retweets.Count))
				.ForMember(dest => dest.ReplyCount, opt => opt.MapFrom(src => src.Replies.Count));

			CreateMap<Tweet, RetweetToReturnDto>()
				.ForMember(dest => dest.TweetId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.FullName))
				.ForMember(dest => dest.UserProfilePictureUrl, opt => opt.MapFrom<TweetUserProfilePictureUrlResolver>())
				.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<TweetPictureUrlResolver>())
				.ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => src.Likes.Count))
				.ForMember(dest => dest.RetweetCount, opt => opt.MapFrom(src => src.Retweets.Count))
				.ForMember(dest => dest.ReplyCount, opt => opt.MapFrom(src => src.Replies.Count));

			CreateMap<UpdateTweetDto, Tweet>();

			CreateMap<Retweet, RetweetToReturnDto>()
				.ForMember(dest => dest.RetweetId, opt => opt.MapFrom(src => src.Id))
				.ForMember(dest => dest.RetweetUserName, opt => opt.MapFrom(src => src.User.FullName))
				.ForMember(dest => dest.RetweetUserProfilePictureUrl, opt => opt.MapFrom<RetweetUserProfilePictureUrlResolver>())
				.ForMember(dest => dest.RetweetedAt, opt => opt.MapFrom(src => src.RetweetedAt))
				.ForMember(dest => dest.Comment, opt => opt.MapFrom(src => src.Comment))
				.ForMember(dest => dest.LikeCount, opt => opt.MapFrom(src => src.Likes.Count))
				.ForMember(dest => dest.ReplyCount, opt => opt.MapFrom(src => src.Replies.Count))
				// ↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
				.ForMember(dest => dest.TweetId, opt => opt.MapFrom(src => src.OriginalTweetId))
				.ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.OriginalTweet.User.FullName))
				.ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.OriginalTweet.Content))
				.ForMember(dest => dest.UserProfilePictureUrl, opt => opt.MapFrom<RetweetFromTweetUserProfilePictureUrlResolver>())
				.ForMember(dest => dest.ImageUrl, opt => opt.MapFrom<RetweetFromTweetPictureUrlResolver>());


            CreateMap<Notification, NotificationDto>()
                .ForMember(dest => dest.UserId, opt => opt.MapFrom(src => src.User!.Id))
                .ForMember(dest => dest.TriggerUserId, opt => opt.MapFrom(src => src.TriggerUser!.Id))
                .ForMember(dest => dest.TweetId, opt => opt.MapFrom(src => src.TweetId!))
                .ForMember(dest => dest.FullNameForUser, opt => opt.MapFrom(src => src.User!.FullName))
                .ForMember(dest => dest.FullNameForTriggerUser, opt => opt.MapFrom(src => src.TriggerUser!.FullName))
                .ForMember(dest => dest.ProfilePictureUrlForUser, opt => opt.MapFrom<UserNotificationPictureUrlResolver>())
                .ForMember(dest => dest.ProfilePictureUrlForTriggerUser, opt => opt.MapFrom<TriggerUserNotificationPictureUrlResolver>());



            CreateMap<Hashtag, HashtagDto>().ReverseMap();
            CreateMap<Hashtag, HashtagToReturn>().ReverseMap();







        }
    }

}
