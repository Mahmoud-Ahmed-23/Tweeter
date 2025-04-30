namespace Tweeter.Core.Application.Abstraction.Dtos.Identity.Account
{
    public record ChangePasswordDto(string CurrentPassword, string NewPassword);

}
