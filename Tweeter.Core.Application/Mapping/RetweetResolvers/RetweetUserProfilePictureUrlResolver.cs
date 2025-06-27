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
	public class RetweetUserProfilePictureUrlResolver(IConfiguration configuration)
		: IValueResolver<Retweet, RetweetToReturnDto, string?>
	{
		public string? Resolve(Retweet source, RetweetToReturnDto destination, string? destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.User?.ProfilePictureUrl))
			{
				return $"{configuration["Urls:ApiBaseUrl"]}/{source.User.ProfilePictureUrl}";
			}

			return string.Empty;
		}
	}
}
