using System;
using System.Collections.Generic;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.OrdersSuggestions.Commands
{
    public class AcceptOrderSuggestionCommand:ShoppingCommandBase
    {
        public Guid OrderSuggestionId { get; set; }
        public List<Guid> OrderSuggestionItems { get; set; }
    }
}