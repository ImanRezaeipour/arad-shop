using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Persons.Commands.Customer
{
    public class DeleteCustomerAddressCommand : ShoppingCommandBase
    {
        public Guid UserId { get; set; }
        public Guid CustomerAddressId { get; set; }
    }
}