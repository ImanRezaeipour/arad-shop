using FluentValidation;
using Shopping.Commands.Commands.Categories.Commands;

namespace Shopping.Commands.Commands.Categories.Validators
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(item => item.Name).NotEmpty().WithMessage("نام دسته بندی نمی تواند خالی باشد");
        }
    }
}