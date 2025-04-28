using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Tweeter.Core.Application.Abstraction.Dtos.Identity;
using Tweeter.Core.Application.Abstraction.Services.Identity.Authentication;
using Tweeter.Core.Domain.Entities.Identity;
using Tweeter.Shared.Results;
using Tweeter.Shared.Settings;

namespace Tweeter.Core.Application.Services.Identity.Authentication
{
	internal class AuthenticationService : IAuthenticationService
	{
		private readonly UserManager<ApplicationUser> _userManager;
		private readonly SignInManager<ApplicationUser> _signInManager;
		private readonly JwtSettings _jwtSettings;

		public AuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, JwtSettings jwtSettings)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_jwtSettings = jwtSettings;
		}

		public async Task<Result<ReturnUserDto>> Login(string email, string password)
		{
			var user = await _userManager.FindByEmailAsync(email);

			if (user is null)
				return Result<ReturnUserDto>.Fail("User not found", ErrorType.NotFound);

			var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

			//if (!user.EmailConfirmed)
			//	return Result<ReturnUserDto>.Fail("Email is not confirmed");

			if (result.IsLockedOut)
				return Result<ReturnUserDto>.Fail("User is locked out", ErrorType.BadRequest);


			if (!result.Succeeded)
				return Result<ReturnUserDto>.Fail("Invalid password", ErrorType.BadRequest);

			return Result<ReturnUserDto>.Success(new ReturnUserDto
			{
				Id = user.Id,
				Email = user.Email!,
				FullName = user.FullName,
				PhoneNumber = user.PhoneNumber,
				ProfilePictureUrl = user.ProfilePictureUrl,
				Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),
				Token = await GenerateToken(user)
			});
		}

		private async Task<string> GenerateToken(ApplicationUser user)
		{
			var roles = await _userManager.GetRolesAsync(user);
			var userClaims = await _userManager.GetClaimsAsync(user);

			var roleClaims = roles.Select(role => new Claim(ClaimTypes.Role, role)).ToList();

			var claims = new List<Claim>
			{
				new Claim(ClaimTypes.PrimarySid, user.Id),
				new Claim(ClaimTypes.Email, user.Email!),
				new Claim(ClaimTypes.MobilePhone,user.PhoneNumber!),
				new Claim(ClaimTypes.Name, user.FullName)
			}
			.Union(userClaims)
			.Union(roleClaims);


			var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));

			var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken(
				issuer: _jwtSettings.Issuer,
				audience: _jwtSettings.Audience,
				expires: DateTime.Now.AddMinutes(_jwtSettings.DurationInDays),
				claims: claims,
				signingCredentials: creds
			);
			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}
