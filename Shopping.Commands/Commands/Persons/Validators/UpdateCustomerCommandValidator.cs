using System.Collections.Generic;
using System.Linq;
using FluentValidation;
using Shopping.Commands.Commands.Persons.Commands.Customer;
using Shopping.Commands.Commands.Persons.Commands.Customer.Items;

namespace Shopping.Commands.Commands.Persons.Validators
{
    public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
    {
        public UpdateCustomerCommandValidator()
        {
            RuleFor(p => p.CustomerAddresses).Must(CheckCustomerAddress)
                .WithMessage("فقط یک آدرس دیفالت میتواند داشته باشد");
        }
        private bool CheckCustomerAddress(List<CustomerAddressItem> address)
        {
            if (!address.Any())
            {
                return true;
            }
            if (address.Count(p=>p.IsDefault) > 1)
            {
                return false;
            }
            return true;
        }
    }
}