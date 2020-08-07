using System;
using Shopping.Infrastructure.Enum;

namespace Shopping.Commands.Commands.OrdersSuggestions.Commands
{
    public class OrderSuggestionItemCommand
    {
        public int Quantity { get; set; }
        public Guid ProductId { get; set; }
        public Guid OrderItemId { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public OrderSuggestionItemType OrderSuggestionItemType { get; set; }
    }
}