using FluentValidation;
using Tweeter.Core.Application.Features.Hashtages.Commands.Models;

namespace Tweeter.Core.Application.Features.Hashtages.Commands.Validators
{
    public class CreateHashtagDtoValidator : AbstractValidator<CreateHashtagCommand>
    {
        public CreateHashtagDtoValidator()
        {


            RuleFor(x => x.HashtagDto)
                .NotNull().WithMessage("HashtagDto is required.")
                .NotEmpty().WithMessage("HashtagDto cannot be empty.");
            RuleFor(x => x.HashtagDto.TagName)
                .NotEmpty().WithMessage("Hashtag name is required.")
                .NotNull().WithMessage("Hashtag name cannot be null.")
                .MaximumLength(50).WithMessage("Hashtag name cannot exceed 50 characters.");

            RuleFor(x => x.HashtagDto.TagName)
                .Matches(@"^#\w+$").WithMessage("Hashtag name must start with '#' and contain only alphanumeric characters and underscores.")
                .When(x => !string.IsNullOrEmpty(x.HashtagDto.TagName)); // Only validate if TagName is not null or empty


        }
    }
}
