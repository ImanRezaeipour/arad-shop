using System;
using System.Collections.Generic;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.FakeIos.Orders.Commands
{
    public class CreateFakeOrderIosCommand : ShoppingCommandBase
    {
        public string FullName { get; set; }
        public Guid CustomerId { get; set; }
        public string AddressText { get; set; }
        public double AddressLat { get; set; }
        public double AddressLng { get; set; }
        public List<FakeOrderIosItemCommandItem> Items { get; set; }
    }
}