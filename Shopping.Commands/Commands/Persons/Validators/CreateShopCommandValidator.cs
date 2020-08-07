using FluentValidation;
using Shopping.Commands.Commands.Persons.Commands.Shop;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Persons.Validators
{
    public class CreateShopCommandValidator:AbstractValidator<CreateShopCommand>
    {
        public CreateShopCommandValidator()
        {
            RuleFor(item => item.NationalCode).Must(item => item.IsValidNationalCode())
                .WithMessage("کد ملی وارد شده صحیح نمی باشد");
        }
    }
}