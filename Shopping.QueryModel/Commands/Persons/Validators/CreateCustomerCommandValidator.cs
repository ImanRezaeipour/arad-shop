using FluentValidation;
using Shopping.Commands.Commands.Persons.Commands.Customer;

namespace Shopping.Commands.Commands.Persons.Validators
{
    public class CreateCustomerCommandValidator:AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerCommandValidator()
        {
            RuleFor(p => p.FirstName).NotEmpty().WithMessage("نام نمی تواند حالی باشد");
            RuleFor(p => p.LastName).NotEmpty().WithMessage("نام خانوادگی نمی تواند حالی باشد");
            RuleFor(p => p.MobileNumber).NotEmpty().WithMessage("شماره تلفن نمی تواند حالی باشد");
            RuleFor(p => p.CustomerAddress.AddressText).NotEmpty().WithMessage("آدرس نمی تواند حالی باشد");
            RuleFor(p => p.CustomerAddress.PhoneNumber).NotEmpty().WithMessage("شماره تلفن  گیرنده نمی تواند حالی باشد");


        }
    }
}