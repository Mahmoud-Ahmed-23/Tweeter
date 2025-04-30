using MediatR;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Application.Bases;

namespace Tweeter.Core.Application.Features.Identity.Account.Command.Models
{
	public class RegisterCommand : IRequest<Response<ReturnUserDto>>
	{
		public RegisterDto RegisterDto { get; set; }
		//public string FullName { get; set; }
		//public string Email { get; set; }
		//public string Password { get; set; }
		//public string PhoneNumber { get; set; }
		//public string ProfilePictureUrl { get; set; }
		//public string Role { get; set; }
	}
}
