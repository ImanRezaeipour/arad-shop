using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Persons.Commands.Customer
{
    public class ActiveCustomerCommand : ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}