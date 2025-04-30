using FluentValidation;
using Tweeter.Core.Application.Features.Identity.Authentication.Command.Models;
using Tweeter.Shared._Common;

namespace Tweeter.Core.Application.Features.Identity.Authentication.Command.Validators
{
    public class ResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {
        public ResetPasswordValidator()
        {
            RuleFor(x => x.ResetPasswordByEmailDto.Email)
                .NotEmpty()
                .WithMessage("Email Must Not Empty , Plz Add a {PropertyName}")
                .EmailAddress().WithMessage("Must Be Email Address")
                .Matches(RegexPatterns.Email);
            RuleFor(x => x.ResetPasswordByEmailDto.NewPassword)
                .NotEmpty()
                .WithMessage("New Password Must Not Empty , Plz Add a {PropertyName}")
                .Matches(RegexPatterns.Password).WithMessage("Password Must Be At Least 8 Characters, Contain At Least One Digit, And Can Include Special Characters");

        }
    }

}
