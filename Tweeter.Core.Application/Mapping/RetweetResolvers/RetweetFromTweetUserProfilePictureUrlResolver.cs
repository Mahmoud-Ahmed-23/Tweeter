using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Application.Mapping.RetweetResolvers
{
	public class RetweetFromTweetUserProfilePictureUrlResolver

		(IConfiguration configuration)
		: IValueResolver<Retweet, RetweetToReturnDto, string?>
	{
		public string? Resolve(Retweet source, RetweetToReturnDto destination, string? destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.OriginalTweet.User?.ProfilePictureUrl))
			{
				return $"{configuration["Urls:TwitterUrl"]}/{source.OriginalTweet.User.ProfilePictureUrl}";
			}

			return string.Empty;
		}
	}
}
