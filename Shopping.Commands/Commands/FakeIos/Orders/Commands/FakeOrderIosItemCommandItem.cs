using System;

namespace Shopping.Commands.Commands.FakeIos.Orders.Commands
{
    public class FakeOrderIosItemCommandItem
    {
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Brand { get; set; }
        public int Quantity { get; set; }
    }
}