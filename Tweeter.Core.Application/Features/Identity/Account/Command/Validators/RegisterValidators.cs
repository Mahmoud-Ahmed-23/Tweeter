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
	public class RegisterValidators : AbstractValidator<RegisterCommand>
	{

		public RegisterValidators()
		{
			ApplyValidationRules();
		}
		public void ApplyValidationRules()
		{
			RuleFor(x => x.RegisterDto.FullName)
				.NotEmpty()
				.WithMessage("FullName Must Not Empty , Plz Add a {PropertyName}");

			RuleFor(x => x.RegisterDto.Email)
				.NotEmpty()
				.WithMessage("\"Email Must Not Empty , Plz Add a {PropertyName}\"")
				.EmailAddress().WithMessage("Must Be Email Address")
				.Matches(RegexPatterns.Email,
		 RegexOptions.IgnoreCase).WithMessage("Invalid Email Address,Only Gmail/Google or Egyptian university emails (@____.edu.eg) are allowed");


			RuleFor(x => x.RegisterDto.Password)
				.NotEmpty()
				.WithMessage("\"Password Must Not Empty , Plz Add a {PropertyName}\"")
				.Matches(RegexPatterns.Password).WithMessage("Password Must Be At Least 8 Characters, Contain At Least One Digit, And Can Include Special Characters");

			RuleFor(x => x.RegisterDto.PhoneNumber)
				.NotEmpty()
				.WithMessage("PhoneNumber Must Not Empty , Plz Add a {PropertyName}")
				.Matches(RegexPatterns.PhoneNumber).WithMessage("Invalid Egyptian phone number.");

			RuleFor(x => x.RegisterDto.ProfilePictureUrl)
				.Matches(RegexPatterns.ProfilePictureUrl)
				.When(x => !string.IsNullOrEmpty(x.RegisterDto.ProfilePictureUrl))
				.WithMessage("Invalid image URL. Please provide a valid URL ending with .jpg, .jpeg, .png, or .gif.");

		}
	}
}
