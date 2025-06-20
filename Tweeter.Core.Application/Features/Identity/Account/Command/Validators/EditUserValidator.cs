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
	public class EditUserValidator : AbstractValidator<EditUserCommand>
	{
		public EditUserValidator()
		{
			CascadeMode = CascadeMode.Stop;

			RuleFor(x => x.EditUserDto.Id)
				.NotEmpty()
				.WithMessage("Id Must Not Empty , Plz Add a {PropertyName}")
				.NotNull()
				.WithMessage("Id Must Not null , Plz Add a {PropertyName}");

			RuleFor(x => x.EditUserDto.FullName)
				.NotEmpty()
				.WithMessage("FullName Must Not Empty , Plz Add a {PropertyName}");

			RuleFor(x => x.EditUserDto.Email)
				.NotEmpty()
				.WithMessage("\"Email Must Not Empty , Plz Add a {PropertyName}\"")
				.EmailAddress().WithMessage("Must Be Email Address")
				.Matches(RegexPatterns.Email,
		 RegexOptions.IgnoreCase).WithMessage("Invalid Email Address,Only Gmail/Google or Egyptian university emails (@____.edu.eg) are allowed");


			RuleFor(x => x.EditUserDto.PhoneNumber)
				.NotEmpty()
				.WithMessage("PhoneNumber Must Not Empty , Plz Add a {PropertyName}")
				.Matches(RegexPatterns.PhoneNumber).WithMessage("Invalid Egyptian phone number.");

			//RuleFor(x => x.EditUserDto.ProfilePictureUrl)
			//	.Matches(RegexPatterns.ProfilePictureUrl)
			//	.When(x => !string.IsNullOrEmpty(x.EditUserDto.ProfilePictureUrl))
			//	.WithMessage("Invalid image URL. Please provide a valid URL ending with .jpg, .jpeg, .png, or .gif."); ;
		}
	}
}
