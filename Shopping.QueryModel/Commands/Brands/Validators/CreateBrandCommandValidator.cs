using FluentValidation;
using Shopping.Commands.Commands.Brands.Commands;

namespace Shopping.Commands.Commands.Brands.Validators
{
    public class CreateBrandCommandValidator : AbstractValidator<CreateBrandCommand>
    {
        public CreateBrandCommandValidator()
        {
            RuleFor(i => i.Name).NotEmpty().WithMessage("عنوان نمی تواند خالی باشد");
            RuleFor(i => i.LatinName).NotEmpty().WithMessage("عنوان انگلیسی نمی تواند خالی باشد");
        }
    }
}