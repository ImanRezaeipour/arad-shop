using System;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.FakeIos.Orders.Commands
{
    public class ChangeFakeOrderIosCommand : ShoppingCommandBase
    {
        public Guid OrderId { get; set; }
        public FakeOrderIosState State { get; set; }
    }
}