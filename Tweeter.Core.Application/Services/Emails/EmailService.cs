using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using Tweeter.Core.Application.Abstraction.Dtos._Common.Emails;
using Tweeter.Core.Application.Abstraction.Services.Emails;
using Tweeter.Shared.Settings;

namespace Tweeter.Core.Application.Services.Emails
{
    public class EmailService(IOptions<MailSettings> emailsettings) : IEmailService
    {
        private readonly MailSettings _mailSettings = emailsettings.Value;
        public async Task SendEmail(Email emailDto)
        {

            var Email = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_mailSettings.Email),
                Subject = emailDto.Subject
            };

            Email.To.Add(MailboxAddress.Parse(emailDto.To));
            Email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));


            var EmailBody = new BodyBuilder();
            EmailBody.TextBody = emailDto.Body;


            Email.Body = EmailBody.ToMessageBody();


            using var Smtp = new SmtpClient();

            Smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

            await Smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);


            await Smtp.AuthenticateAsync(_mailSettings.Email, _mailSettings.Password);


            await Smtp.SendAsync(Email);


            await Smtp.DisconnectAsync(true);
        }
    }
}
