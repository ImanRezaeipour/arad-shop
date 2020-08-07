using System;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Infrastructure.Types;

namespace Shopping.Commands.Commands.Persons.Commands.Customer
{
    public class AddCustomerAddressCommand : ShoppingCommandBase
    {
        public string Title { get; set; }
        public Guid UserId { get; set; }
        public string AddressText { get; set; }
        public string PhoneNumber { get; set; }
        public Guid CityId { get; set; }
        public Position Position { get; set; }
    }
}