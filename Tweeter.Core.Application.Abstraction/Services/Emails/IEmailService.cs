using Tweeter.Core.Application.Abstraction.Dtos._Common.Emails;

namespace Tweeter.Core.Application.Abstraction.Services.Emails
{
    public interface IEmailService
    {
        public Task SendEmail(Email email);

    }
}
