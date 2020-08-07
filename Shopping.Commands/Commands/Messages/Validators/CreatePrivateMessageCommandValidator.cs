using FluentValidation;
using Shopping.Commands.Commands.Messages.Commands;

namespace Shopping.Commands.Commands.Messages.Validators
{
    public class CreatePrivateMessageCommandValidator:AbstractValidator<CreatePrivateMessageCommand>
    {
        public CreatePrivateMessageCommandValidator()
        {
            RuleFor(p => p.Title).NotEmpty().WithMessage("عنوان پیام نمی توان خالی باشد");
            RuleFor(p=>p.Body).NotEmpty().WithMessage("متن پیام نمی تواند خالی باشد");
        }
    }
}