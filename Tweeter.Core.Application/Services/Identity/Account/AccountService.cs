using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Identity;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Core.Application.Abstraction.Services.Identity.Account;
using Tweeter.Core.Application.Bases;
using Tweeter.Core.Domain.Entities.Identity;
using Tweeter.Shared.ErrorModule.Exeptions;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Services.Identity.Account
{
	internal class AccountService : IAccountService
	{
		private readonly UserManager<ApplicationUser> _userManager;

		public AccountService(UserManager<ApplicationUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task<Result<ReturnUserDto>> Register(RegisterDto registerDto)
		{
			var isExist = await _userManager.FindByEmailAsync(registerDto.Email);

			if (isExist is not null)
			{
				return Result<ReturnUserDto>.Fail("User already exists", ErrorType.NotFound);
			}

			var user = new ApplicationUser
			{
				UserName = registerDto.Email,
				Email = registerDto.Email,
				PhoneNumber = registerDto.PhoneNumber,
				FullName = registerDto.FullName,
				ProfilePictureUrl = registerDto.ProfilePictureUrl,
			};

			var result = await _userManager.CreateAsync(user, registerDto.Password);

			if (!result.Succeeded)
				return Result<ReturnUserDto>.Fail("Can't Create an Account :(", ErrorType.BadRequest);


			await _userManager.AddToRoleAsync(user, registerDto.Role);

			return Result<ReturnUserDto>.Success(new ReturnUserDto
			{
				Id = user.Id,
				Email = user.Email,
				FullName = user.FullName,
				PhoneNumber = user.PhoneNumber,
				ProfilePictureUrl = user.ProfilePictureUrl,
				Role = registerDto.Role
			});

		}
	}
}
