using AutoMapper;
using Microsoft.Extensions.Configuration;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Application.Mapping.TweetResolvers
{
	public class TweetPictureUrlResolver(IConfiguration configuration) : IValueResolver<Tweet, TweetToReturnDto, string>
	{
		public string Resolve(Tweet source, TweetToReturnDto destination, string destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.ImageUrl))
			{
				return $"{configuration["Urls:ApiBaseUrl"]}/{source.ImageUrl}";
			}
			return string.Empty;
		}
	}
}
