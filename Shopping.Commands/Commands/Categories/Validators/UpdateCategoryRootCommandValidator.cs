using FluentValidation;
using Shopping.Commands.Commands.Categories.Commands;

namespace Shopping.Commands.Commands.Categories.Validators
{
    public class UpdateCategoryRootCommandValidator:AbstractValidator<UpdateCategoryRootCommand>
    {
        public UpdateCategoryRootCommandValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("نام دسته نمی تواند خالی باشد");
            
        }
    }
}