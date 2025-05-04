using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
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

        public async Task<Result<ChangePasswordToReturn>> ChangePasswordAsync(ClaimsPrincipal claims, ChangePasswordDto changePasswordDto)
        {
            var userId = claims.FindFirst(ClaimTypes.PrimarySid)?.Value;

            if (userId is null) return Result<ChangePasswordToReturn>.Fail("UnAuthorized , You Are Not Allowed", ErrorType.Unauthorized);


            // Retrieve the user from the database
            var user = await _userManager.FindByIdAsync(userId);

            if (user is null) return Result<ChangePasswordToReturn>.Fail("User Not Found", ErrorType.NotFound);


            // Verify the current password
            var isCurrentPasswordValid = await _userManager.CheckPasswordAsync(user, changePasswordDto.CurrentPassword);

            if (!isCurrentPasswordValid)
            {
                return Result<ChangePasswordToReturn>.Fail("Current password is incorrect", ErrorType.BadRequest);
            }

            // Change the password
            var result = await _userManager.ChangePasswordAsync(user, changePasswordDto.CurrentPassword, changePasswordDto.NewPassword);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return Result<ChangePasswordToReturn>.Fail($"Failed to change password: {errors}", ErrorType.BadRequest);
            }

            // Optionally, generate a new token for the user
            var newToken = await GenerateToken(user);

            return Result<ChangePasswordToReturn>.Success(new ChangePasswordToReturn(
                Message: "Password changed successfully",
                Token: newToken


                ));



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



        public async Task<Result<ReturnUserDto>> ResetPasswordByEmailAsync(ResetPasswordByEmailDto resetCodeDto)
        {
            var user = await _userManager.Users.Where(u => u.Email == resetCodeDto.Email).FirstOrDefaultAsync();

            if (user is null)
                return Result<ReturnUserDto>.Fail("User Not Found", ErrorType.NotFound);

            var RemovePass = await _userManager.RemovePasswordAsync(user);

            if (!RemovePass.Succeeded)
                return Result<ReturnUserDto>.Fail("Something Went Wrong While Reseting Your Password", ErrorType.BadRequest);

            var newPass = await _userManager.AddPasswordAsync(user, resetCodeDto.NewPassword);

            if (!newPass.Succeeded)
                return Result<ReturnUserDto>.Fail("Something Went Wrong While Reseting Your Password", ErrorType.BadRequest);

            var mappedUser = new ReturnUserDto
            {
                FullName = user.FullName!,
                Id = user.Id,
                Email = user.Email!,
                Token = await GenerateToken(user),
                PhoneNumber = user.PhoneNumber,
                ProfilePictureUrl = user.ProfilePictureUrl,
                Role = (await _userManager.GetRolesAsync(user)).FirstOrDefault(),

            };

            //if (user!.RefreshTokens.Any(t => t.IsActice))
            //{
            //    var acticetoken = user.RefreshTokens.FirstOrDefault(x => x.IsActice);
            //    mappedUser.RefreshToken = acticetoken!.Token;
            //    mappedUser.RefreshTokenExpirationDate = acticetoken.ExpireOn;
            //}
            //else
            //{

            //    var refreshtoken = GenerateRefreshToken();
            //    mappedUser.RefreshToken = refreshtoken.Token;
            //    mappedUser.RefreshTokenExpirationDate = refreshtoken.ExpireOn;

            //    user.RefreshTokens.Add(new RefreshToken()
            //    {
            //        Token = refreshtoken.Token,
            //        ExpireOn = refreshtoken.ExpireOn,
            //    });
            //    await userManager.UpdateAsync(user);
            //}

            return Result<ReturnUserDto>.Success(mappedUser);

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


        public async Task<Result<SuccessDto>> LougOutAsync(ClaimsPrincipal claimsPrincipal)
        {
            try
            {
                var userId = claimsPrincipal.FindFirst(ClaimTypes.PrimarySid)?.Value;

                if (string.IsNullOrEmpty(userId))
                {
                    return Result<SuccessDto>.Fail("User not authenticated", ErrorType.Unauthorized);
                }

                var user = await _userManager.FindByIdAsync(userId);
                if (user is null)
                {
                    return Result<SuccessDto>.Fail("User not found", ErrorType.NotFound);
                }

                await _signInManager.SignOutAsync();



                return Result<SuccessDto>.Success(new SuccessDto(
                    "Success",
                    "Logged out successfully"));
            }
            catch (Exception ex)
            {
                return Result<SuccessDto>.Fail("Error during logout", ErrorType.Unexpected);
            }
        }

        public async Task<Result<ReturnUserDto>> GetCurrentUser(ClaimsPrincipal claimsPrincipal)
        {
            var email = claimsPrincipal.FindFirstValue(ClaimTypes.Email);
            var user = await _userManager.FindByEmailAsync(email!);

            if (user is null)
                return Result<ReturnUserDto>.Fail("User not found", ErrorType.NotFound);


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
    }
}
