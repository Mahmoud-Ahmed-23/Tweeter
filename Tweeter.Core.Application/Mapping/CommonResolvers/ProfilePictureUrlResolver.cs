using AutoMapper;
using Microsoft.Extensions.Configuration;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Domain.Entities.Identity;

namespace Tweeter.Core.Application.Mapping.CommonResolvers
{
	public class ProfilePictureUrlResolver(IConfiguration configuration) : IValueResolver<ApplicationUser, ReturnUserDto, string?>
	{
		public string Resolve(ApplicationUser source, ReturnUserDto destination, string? destMember, ResolutionContext context)
		{
			if (!string.IsNullOrEmpty(source.ProfilePictureUrl))
			{
				return $"{configuration["Urls:TwitterUrl"]}/{source.ProfilePictureUrl}";
			}
			return string.Empty;
		}
	}
}
