using System;

namespace Shopping.Commands.Commands.OrdersSuggestions.Commands
{
    public class AcceptOrderSuggestionItemCommand
    {
        public Guid OrderSuggestionItemId { get; set; }
        public int Quantity { get; set; }
    }
}