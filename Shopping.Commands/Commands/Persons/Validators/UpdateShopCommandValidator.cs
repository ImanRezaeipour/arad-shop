using FluentValidation;
using Shopping.Commands.Commands.Persons.Commands.Shop;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Persons.Validators
{
    public class UpdateShopCommandValidator : AbstractValidator<UpdateShopCommand>
    {
        public UpdateShopCommandValidator()
        {
            RuleFor(item => item.NationalCode).Must(item => item.IsValidNationalCode())
                .WithMessage("کد ملی وارد شده صحیح نمی باشد");
        }
    }
}