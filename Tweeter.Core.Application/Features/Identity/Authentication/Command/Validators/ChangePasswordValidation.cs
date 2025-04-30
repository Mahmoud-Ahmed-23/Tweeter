using FluentValidation;
using Tweeter.Core.Application.Features.Identity.Authentication.Command.Models;
using Tweeter.Shared._Common;

namespace Tweeter.Core.Application.Features.Identity.Authentication.Command.Validators
{
    public class ChangePasswordValidation : AbstractValidator<ChangePasswordCommand>
    {
        public ChangePasswordValidation()
        {

            RuleFor(x => x.ChangePasswordDto.CurrentPassword)
                .NotEmpty()
                .WithMessage("\"Password Must Not Empty , Plz Add a {PropertyName}\"");

            RuleFor(x => x.ChangePasswordDto.NewPassword)
                .NotEmpty()
                .WithMessage("\"Password Must Not Empty , Plz Add a {PropertyName}\"")
                .Matches(RegexPatterns.Password).WithMessage("Password Must Be At Least 8 Characters, Contain At Least One Digit, And Can Include Special Characters");


        }
    }

}
