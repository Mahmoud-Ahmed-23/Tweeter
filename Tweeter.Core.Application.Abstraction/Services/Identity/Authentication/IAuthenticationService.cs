using Tweeter.Core.Application.Abstraction.Dtos.Identity;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Abstraction.Services.Identity.Authentication
{
	public interface IAuthenticationService
	{
		Task<Result<ReturnUserDto>> Login(string email, string password);
	}
}
