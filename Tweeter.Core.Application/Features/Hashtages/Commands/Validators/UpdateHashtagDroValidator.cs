using FluentValidation;
using Tweeter.Core.Application.Features.Hashtages.Commands.Models;

namespace Tweeter.Core.Application.Features.Hashtages.Commands.Validators
{
    public class UpdateHashtagDroValidator : AbstractValidator<UpdateHashtagCommand>
    {
        public UpdateHashtagDroValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .GreaterThan(0).WithMessage("Id must be greater than 0.");
            RuleFor(x => x.HashtagDto)
                .NotNull().WithMessage("HashtagDto is required.");
            RuleFor(x => x.HashtagDto.TagName)
                .NotEmpty().WithMessage("TagName is required.")
                .MaximumLength(50).WithMessage("TagName must not exceed 50 characters.");
            RuleFor(x => x.HashtagDto.NormalizedTagName)
                .NotEmpty().WithMessage("NormalizedTagName is required.")
                .MaximumLength(50).WithMessage("NormalizedTagName must not exceed 50 characters.");

            RuleFor(x => x.HashtagDto.TagName)
                .Matches(@"^#\w+$").WithMessage("TagName must start with '#' and contain only alphanumeric characters and underscores.")
                .When(x => !string.IsNullOrEmpty(x.HashtagDto.TagName)); // Only validate if TagName is not null or empty

        }

    }
}
