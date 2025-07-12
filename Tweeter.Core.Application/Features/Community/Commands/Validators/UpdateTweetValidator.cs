using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Features.Community.Commands.Models;

namespace Tweeter.Core.Application.Features.Community.Commands.Validators
{
	public class UpdateTweetValidator : AbstractValidator<UpdateTweetCommand>
	{
		public UpdateTweetValidator()
		{
			RuleFor(x => x.Id)
				.NotEmpty().WithMessage("TweetId is required.")
				.NotNull().WithMessage("TweetId cannot be null.")
				.GreaterThan(0).WithMessage("TweetId must be greater than 0.");

			RuleFor(x => x.TweetDto.Content)
				.NotEmpty().WithMessage("Content is required.")
				.NotNull().WithMessage("Content cannot be null.")
				.MaximumLength(280).WithMessage("Content cannot exceed 280 characters.");

			RuleFor(x => x.TweetDto.ImageUrl)
				.NotEmpty().WithMessage("ImageUrl is required.")
				.NotNull().WithMessage("ImageUrl cannot be null.");
		}
	}
}
