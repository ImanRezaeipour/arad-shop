using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using Shopping.Commands.Commands.Persons.Commands.Customer.Items;
using Shopping.Commands.Commands.Persons.Validators;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Persons.Commands.Customer
{
    [Validator(typeof(UpdateCustomerCommandValidator))]
    public class UpdateCustomerCommand: ShoppingCommandBase
    {
        public Guid CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public DateTime BirthDate { get; set; }
        public List<CustomerAddressItem> CustomerAddresses { get; set; }
    }
}