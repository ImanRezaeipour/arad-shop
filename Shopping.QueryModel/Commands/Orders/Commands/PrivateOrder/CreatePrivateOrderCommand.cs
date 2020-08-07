using System;
using Shopping.Commands.Commands.Orders.Commands.Abstract;

namespace Shopping.Commands.Commands.Orders.Commands.PrivateOrder
{
    public class CreatePrivateOrderCommand : OrderBaseCommand
    {
        public Guid ShopId { get; set; }
    }
}