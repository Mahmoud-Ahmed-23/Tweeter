using FluentValidation;
using Tweeter.Core.Application.Features.Identity.Account.Command.Models;
using Tweeter.Shared._Common;

namespace Tweeter.Core.Application.Features.Identity.Account.Command.Validators
{
    public class VerifyCodeByEmailValidator : AbstractValidator<VerifiyCodeByEmailCommand>
    {
        public VerifyCodeByEmailValidator()
        {
            RuleFor(x => x.ResetCodeConfirmationByEmailDto.Email)
                .NotEmpty()
                .WithMessage("Email Must Not Empty , Plz Add a {PropertyName}")
                .EmailAddress().WithMessage("Must Be Email Address")
                .Matches(RegexPatterns.Email);












        }
    }

}
