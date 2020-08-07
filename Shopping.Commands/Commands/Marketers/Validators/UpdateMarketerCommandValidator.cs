using FluentValidation;
using Shopping.Commands.Commands.Marketers.Commands;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Marketers.Validators
{
    public class UpdateMarketerCommandValidator : AbstractValidator<UpdateMarketerCommand>
    {
        public UpdateMarketerCommandValidator()
        {
            RuleFor(item => item.NationalCode).Must(item => item.IsValidNationalCode())
                .WithMessage("کد ملی وارد شده صحیح نمی باشد");
        }
    }
}