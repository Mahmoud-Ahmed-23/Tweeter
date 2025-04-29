using Tweeter.Core.Application.Abstraction.Dtos.Identity;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Abstraction.Services.Identity.Account
{
	public interface IAccountService
	{
		Task<Result<ReturnUserDto>> Register(RegisterDto registerDto);
	}
}
