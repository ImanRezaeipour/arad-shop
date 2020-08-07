using System;
using Shopping.Infrastructure.Types;

namespace Shopping.Commands.Commands.Persons.Commands.Customer.Items
{
    public class CustomerAddressItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Guid CityId { get; set; }
        public string AddressText { get; set; }
        public string PhoneNumber { get; set; }
        public Position Position { get; set; }
        public bool IsDefault { get; set; }
    }
}