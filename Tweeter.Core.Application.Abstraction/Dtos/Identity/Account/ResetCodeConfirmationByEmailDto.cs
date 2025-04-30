namespace Tweeter.Core.Application.Abstraction.Dtos.Identity.Account
{
    public record ResetCodeConfirmationByEmailDto(
        string Email,
        int ResetCode
    );

}
