using AutoMapper;
using Tweeter.Core.Application.Abstraction.Dtos.Following;
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

        }
    }

}
