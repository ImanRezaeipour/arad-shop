using FluentValidation;
using Shopping.Commands.Commands.Categories.Commands;

namespace Shopping.Commands.Commands.Categories.Validators
{
    public class CreateCategoryRootCommandValidator : AbstractValidator<CreateCategoryRootCommand>
    {
        public CreateCategoryRootCommandValidator()
        {
            RuleFor(item => item.Name).NotEmpty().WithMessage("نام دسته بندی نمی تواند خالی باشد");
            RuleFor(item => item.CategoryImage).NotNull().WithMessage("عکس های دسته بندی نمی تواند خالی باشد");
            RuleFor(item => item.CategoryImage.FullMainCatImage).NotEmpty().WithMessage("عکس کامل دسته بندی نمی تواند خالی باشد");
            RuleFor(item => item.CategoryImage.TopPageCatImage).NotEmpty().WithMessage("عکس نوار بالایی دسته بندی نمی تواند خالی باشد");
            RuleFor(item => item.CategoryImage.MainCatImage).NotEmpty().WithMessage("عکس اصلی دسته بندی نمی تواند خالی باشد");
        }
    }
}