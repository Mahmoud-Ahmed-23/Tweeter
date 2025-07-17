using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tweeter.Core.Application.Features.Identity.Account.Command.Models;
using Tweeter.Shared._Common;

namespace Tweeter.Core.Application.Features.Identity.Account.Command.Validators
{
	public class ConfirmUserEmailValidator : AbstractValidator<ConfirmUserEmailCommand>
	{
		public ConfirmUserEmailValidator()
		{
			RuleFor(x => x.Email)
				.NotEmpty().WithMessage("Email is required")
				.EmailAddress().WithMessage("Invalid email format")
				.Matches(RegexPatterns.Email,
						 RegexOptions.IgnoreCase).WithMessage("Invalid Email Address,Only Gmail/Google or Egyptian university emails (@____.edu.eg) are allowed");

			RuleFor(x => x.Code)
				.NotEmpty().WithMessage("Code is required")
				.GreaterThan(0).WithMessage("Code must be a positive integer");
		}
	}
}
