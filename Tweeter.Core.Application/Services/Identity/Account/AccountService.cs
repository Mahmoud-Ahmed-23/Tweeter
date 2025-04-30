using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using Tweeter.Core.Application.Abstraction.Dtos._Common.Emails;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.Account;
using Tweeter.Core.Application.Abstraction.Dtos.Identity.ReturnedDto;
using Tweeter.Core.Application.Abstraction.Services.Emails;
using Tweeter.Core.Application.Abstraction.Services.Identity.Account;
using Tweeter.Core.Domain.Entities.Identity;
using Tweeter.Shared.Results;

namespace Tweeter.Core.Application.Services.Identity.Account
{
    internal class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IEmailService _emailService;

        public AccountService(UserManager<ApplicationUser> userManager, IEmailService emailService)
        {
            _userManager = userManager;
            _emailService = emailService;
        }

        public async Task<Result<SuccessDto>> SendCodeByEmailAsync(ForgetPasswordByEmailDto emailDto)
        {
            // Use emailDto.Email (uppercase E) instead of emailDto.email
            var user = await _userManager.Users.Where(u => u.Email == emailDto.Email).FirstOrDefaultAsync();

            if (user is null)
                return Result<SuccessDto>.Fail("User Not Found", ErrorType.NotFound);

            var resetCode = RandomNumberGenerator.GetInt32(100_000, 999_999);
            var resetCodeExpire = DateTime.UtcNow.AddMinutes(15);

            user.ResetCode = resetCode;
            user.ResetCodeExpiry = resetCodeExpire;
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
                return Result<SuccessDto>.Fail("Can't Update User", ErrorType.BadRequest);

            var email = new Email()
            {
                To = emailDto.Email,  // Fixed here too
                Subject = "Reset Code For Event Management Account",
                Body = $"We Have Received Your Request To Reset Your Account Password, \nYour Reset Code Is ==> [ {resetCode} ] <== \nNote: This Code Will Expire After 15 Minutes!",
            };

            await _emailService.SendEmail(email);

            var successObj = new SuccessDto(
                Status: "Success",
                Message: "We Have Sent You The Reset Code"
            );

            return Result<SuccessDto>.Success(successObj);
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


        public async Task<Result<SuccessDto>> VerifyCodeByEmailAsync(ResetCodeConfirmationByEmailDto resetCodeDto)
        {
            var user = await _userManager.Users.Where(u => u.Email == resetCodeDto.Email).FirstOrDefaultAsync();

            if (user is null)
                return Result<SuccessDto>.Fail("User Not Found", ErrorType.NotFound);


            if (user.ResetCode != resetCodeDto.ResetCode)
                return Result<SuccessDto>.Fail("The Provided Code Is Not Valid", ErrorType.BadRequest);



            if (user.ResetCodeExpiry < DateTime.UtcNow)
                return Result<SuccessDto>.Fail("The Provided Code Is Expired", ErrorType.BadRequest);

            var SuccessObj = new SuccessDto(

                Status: "Success",
                Message: "The Provided Code Is Valid, You Can Reset Your Password Now!"


                );
            return Result<SuccessDto>.Success(SuccessObj);

        }


    }
}
