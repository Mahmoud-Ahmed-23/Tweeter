using AutoMapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Community;
using Tweeter.Core.Domain.Entities.Data;

namespace Tweeter.Core.Application.Mapping.TweetResolvers
{
	public class TweetUserProfilePictureUrlResolver(IConfiguration configuration)
	: IValueResolver<Tweet, TweetToReturnDto, string?>
	{
		public string? Resolve(Tweet source, TweetToReturnDto destination, string? destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.User?.ProfilePictureUrl))
			{
				return $"{configuration["Urls:TwitterUrl"]}/{source.User.ProfilePictureUrl}";
			}

			return string.Empty;
		}
	}

}
