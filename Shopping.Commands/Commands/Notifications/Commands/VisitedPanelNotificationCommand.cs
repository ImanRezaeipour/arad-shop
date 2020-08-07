using System;
using FluentValidation.Attributes;
using Shopping.Commands.Commands.Notifications.Validators;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Notifications.Commands
{
    [Validator(typeof(VisitedPanelNotificationCommandValidator))]
    public class VisitedPanelNotificationCommand : ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}