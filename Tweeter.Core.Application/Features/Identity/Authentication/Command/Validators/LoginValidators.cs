using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Tweeter.Core.Application.Features.Identity.Authentication.Command.Models;
using Tweeter.Shared._Common;

namespace Tweeter.Core.Application.Features.Identity.Authentication.Command.Validators
{
	public class LoginValidators : AbstractValidator<LoginCommand>
	{
		public LoginValidators()
		{
			ApplyValidationRules();
		}
		public void ApplyValidationRules()
		{
			RuleFor(x => x.Email)
				.NotEmpty()
				.WithMessage("\"Email Must Not Empty , Plz Add a {PropertyName}\"")
				.EmailAddress().WithMessage("Must Be Email Address")
				.Matches(RegexPatterns.Email,
		 RegexOptions.IgnoreCase).WithMessage("Invalid Email Address,Only Gmail/Google or Egyptian university emails (@____.edu.eg) are allowed");

			RuleFor(x => x.Password)
				.NotEmpty()
				.WithMessage("\"Password Must Not Empty , Plz Add a {PropertyName}\"");
		}
	}
}
