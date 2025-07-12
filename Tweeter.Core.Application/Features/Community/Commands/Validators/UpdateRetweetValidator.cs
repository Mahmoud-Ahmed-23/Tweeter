using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Features.Community.Commands.Models;

namespace Tweeter.Core.Application.Features.Community.Commands.Validators
{
	public class UpdateRetweetValidator:AbstractValidator<UpdateRetweetCommand>
	{
		public UpdateRetweetValidator()
		{
			RuleFor(x => x.RetweetId)
				.NotEmpty().WithMessage("RetweetId is required.")
				.NotNull().WithMessage("RetweetId cannot be null.")
				.GreaterThan(0).WithMessage("RetweetId must be greater than 0.");
		
			RuleFor(x => x.Content)
				.NotEmpty().WithMessage("Content is required.")
				.NotNull().WithMessage("Content cannot be null.")
				.MaximumLength(280).WithMessage("Content cannot exceed 280 characters.");
		}
	}
}
