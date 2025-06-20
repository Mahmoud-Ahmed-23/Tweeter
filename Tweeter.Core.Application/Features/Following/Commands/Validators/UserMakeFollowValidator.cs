using FluentValidation;
using Tweeter.Core.Application.Features.Following.Commands.Models;

namespace Tweeter.Core.Application.Features.Following.Commands.Validators
{
    public class UserMakeFollowValidator : AbstractValidator<UserMakeFollowCommand>
    {
        public UserMakeFollowValidator()
        {


            RuleFor(x => x.FolloweeId)
                .NotEmpty().WithMessage("FolloweeId is required.")
                .NotNull().WithMessage("FolloweeId cannot be null.");

        }
    }
}
