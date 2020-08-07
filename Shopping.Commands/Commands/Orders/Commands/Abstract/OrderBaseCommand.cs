using System;
using System.Collections.Generic;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Orders.Commands.Abstract
{
    public abstract class OrderBaseCommand : ShoppingCommandBase
    {
        public Guid UserId { get; set; }
        public string Description { get; set; }
        public Guid CustomerAddressId { get; set; }
        public List<OrderItemCommand> OrderItems { get; set; }
    }
}