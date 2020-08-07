using System;
using Shopping.Infrastructure.Types;

namespace Shopping.Commands.Commands.Persons.Commands.Shop.Item
{
    public class ShopAddressCommandItem
    {
        public long ZoneId { get;  set; }
        public Guid CityId { get;  set; }
        public string AddressText { get; set; }
        public string PhoneNumber { get; set; }
        public string ShopMobileNumber { get; set; }
        public Position Position { get; set; }
        public Guid ProvinceId { get; set; }
    }
}