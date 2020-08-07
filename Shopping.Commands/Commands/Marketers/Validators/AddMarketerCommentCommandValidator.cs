using FluentValidation;
using Shopping.Commands.Commands.Marketers.Commands;

namespace Shopping.Commands.Commands.Marketers.Validators
{
    public class AddMarketerCommentCommandValidator : AbstractValidator<AddMarketerCommentCommand>
    {
        public AddMarketerCommentCommandValidator()
        {
            RuleFor(p => p.CommentTitle).NotEmpty().WithMessage("توضیحات نمیتواند خالی باشد");
        }
    }
}