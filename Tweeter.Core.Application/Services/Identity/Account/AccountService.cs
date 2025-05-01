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
                To = emailDto.Email,
                Subject = "Password Reset Code for Your Account",
                Body = BuildResetPasswordEmail(resetCode),
                IsBodyHtml = true
            };

            await _emailService.SendEmail(email);

            var successObj = new SuccessDto(
                Status: "Success",
                Message: "We have sent a password reset code to your email"
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

        private string BuildResetPasswordEmail(int resetCode)
        {
            return $@"
    <!DOCTYPE html>
    <html>
    <head>
        <style>
            body {{ font-family: Arial, sans-serif; line-height: 1.6; color: #333; max-width: 600px; margin: 0 auto; padding: 20px; }}
            .header {{ color: #2c3e50; border-bottom: 1px solid #eee; padding-bottom: 10px; }}
            .code {{ font-size: 24px; font-weight: bold; color: #e74c3c; margin: 20px 0; text-align: center; }}
            .footer {{ margin-top: 20px; font-size: 12px; color: #7f8c8d; border-top: 1px solid #eee; padding-top: 10px; }}
            .button {{ background-color: #3498db; color: white; padding: 10px 20px; text-decoration: none; border-radius: 5px; display: inline-block; }}
        </style>
    </head>
    <body>
        <div class='header'>
            <h2>Password Reset Request</h2>
        </div>
        
        <p>We received a request to reset your password. Please use the following verification code:</p>
        
        <div class='code'>{resetCode}</div>
        
        <p>This code will expire in <strong>15 minutes</strong>. If you didn't request this, please ignore this email or contact support if you have questions.</p>
        
        <div class='footer'>
            <p>Thank you,<br>The Support Team</p>
        </div>
    </body>
    </html>";
        }


    }
}
