using System;
using System.Collections.Generic;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.OrdersSuggestions.Commands
{
    public class CreateOrderSuggestionCommand : ShoppingCommandBase
    {
        public Guid UserId { get; set; }
        public long OrderId { get; set; }
        public int Discount { get; set; }
        public string Description { get; set; }
        public int ShippingTime { get; set; }
        public List<OrderSuggestionItemCommand> OrderSuggestionItems { get; set; }
    }
}