using FluentValidation;
using Tweeter.Core.Application.Features.Notifications.Commands.Models;

namespace Tweeter.Core.Application.Features.Notifications.Commands.Validators
{
    public class DeleteSpecififNotificationQueryValidator : AbstractValidator<DeleteSpecififNotificationQuery>
    {
        public DeleteSpecififNotificationQueryValidator()
        {
            RuleFor(x => x.NotificationId)
                .NotEmpty().WithMessage("Notification ID cannot be empty.")
                .GreaterThan(0).WithMessage("Notification ID must be greater than zero.");
        }
    }
}
