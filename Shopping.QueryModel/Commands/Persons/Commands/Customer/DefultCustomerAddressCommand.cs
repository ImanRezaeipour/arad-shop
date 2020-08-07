using System;
using Shopping.Infrastructure.Types;

namespace Shopping.Commands.Commands.Persons.Commands.Customer
{
    public class DefultCustomerAddressCommand
    {
        public string AddressText { get; set; }
        public string PhoneNumber { get; set; }
        public Guid CityId { get; set; }
        public Position Position { get; set; }
    }
}