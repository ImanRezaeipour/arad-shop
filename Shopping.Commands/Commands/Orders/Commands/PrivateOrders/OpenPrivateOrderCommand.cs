using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Orders.Commands.PrivateOrders
{
    public class OpenPrivateOrderCommand : ShoppingCommandBase
    {
        public Guid UserId { get; set; }
        public long OrderId { get; set; }
    }
}