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
            var email = new MimeMessage()
            {
                Sender = MailboxAddress.Parse(_mailSettings.Email),
                Subject = emailDto.Subject
            };

            email.To.Add(MailboxAddress.Parse(emailDto.To));
            email.From.Add(new MailboxAddress(_mailSettings.DisplayName, _mailSettings.Email));

            var emailBody = new BodyBuilder();

            if (emailDto.IsBodyHtml)
            {
                emailBody.HtmlBody = emailDto.Body;
            }
            else
            {
                emailBody.TextBody = emailDto.Body;
            }

            email.Body = emailBody.ToMessageBody();

            using var smtp = new SmtpClient();
            smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

            await smtp.ConnectAsync(_mailSettings.Host, _mailSettings.Port, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(_mailSettings.Email, _mailSettings.Password);
            await smtp.SendAsync(email);
            await smtp.DisconnectAsync(true);
        }
    }
}
