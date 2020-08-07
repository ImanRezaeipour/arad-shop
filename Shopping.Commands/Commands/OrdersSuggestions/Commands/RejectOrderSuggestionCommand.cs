using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.OrdersSuggestions.Commands
{
    public class RejectOrderSuggestionCommand:ShoppingCommandBase
    {
        public Guid OrderSuggestionId { get; set; }
    }
}