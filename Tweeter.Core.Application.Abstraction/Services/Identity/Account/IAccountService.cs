using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Abstraction.Services.Identity.Account
{
    public interface IAccountService
    {
        Task<Result<ReturnUserDto>> Register(RegisterDto registerDto);

        Task<Result<SuccessDto>> SendCodeByEmailAsync(ForgetPasswordByEmailDto emailDto);
        Task<Result<SuccessDto>> VerifyCodeByEmailAsync(ResetCodeConfirmationByEmailDto resetCodeDto);
    }
}
