using FluentValidation;
using Shopping.Commands.Commands.Notifications.Commands;

namespace Shopping.Commands.Commands.Notifications.Validators
{
    public class VisitedPanelNotificationCommandValidator:AbstractValidator<VisitedPanelNotificationCommand>
    {
        public VisitedPanelNotificationCommandValidator()
        {
            RuleFor(item => item.Id).NotEmpty().WithMessage("شناسه نمی تواند خالی باشد");
        }
    }
}