using System;

namespace Shopping.Commands.Commands.Orders.Commands
{
    public class OrderItemCommand
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
    }
}