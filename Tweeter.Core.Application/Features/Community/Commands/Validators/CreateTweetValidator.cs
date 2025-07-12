using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Features.Community.Commands.Models;

namespace Tweeter.Core.Application.Features.Community.Commands.Validators
{
	public class CreateTweetValidator:AbstractValidator<CreateTweetCommand>
	{
		public CreateTweetValidator()
		{
			RuleFor(x => x.TweetDto.Content)
				.NotEmpty().WithMessage("Content is required.")
				.NotNull().WithMessage("Content cannot be null.")
				.MaximumLength(280).WithMessage("Content cannot exceed 280 characters.")
				.When(x => x.TweetDto.ImageUrl is null);

			RuleFor(x => x.TweetDto.ImageUrl)
				.NotEmpty().WithMessage("ImageUrl is required.")
				.NotNull().WithMessage("ImageUrl cannot be null.")
				.When(x => x.TweetDto.Content is null);
		}
	}
}
