using System.Security.Claims;
using Tweeter.Core.Application.Abstraction.Common;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.RefreshToken;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Abstraction.Services.Identity.Authentication
{
	public interface IAuthenticationService
	{
		Task<Result<ReturnUserDto>> Login(string email, string password);
		Task<Result<ReturnUserDto>> ResetPasswordByEmailAsync(ResetPasswordByEmailDto resetCodeDto);

		Task<Result<ChangePasswordToReturn>> ChangePasswordAsync(ClaimsPrincipal claims, ChangePasswordDto changePasswordDto);

		Task<Result<SuccessDto>> LougOutAsync(ClaimsPrincipal claimsPrincipal);

		Task<Result<ReturnUserDto>> GetCurrentUser(ClaimsPrincipal claimsPrincipal);

		Task<Result<UserProfileToReturn>> GetUserProfile(SpecParams specParams);

		Task<Result<ReturnUserDto>> GetRefreshToken(RefreshDto refreshDto, CancellationToken cancellationToken);
		Task<Result<bool>> RevokeRefreshToken(RefreshDto refreshDto, CancellationToken cancellationToken);

	}
}
