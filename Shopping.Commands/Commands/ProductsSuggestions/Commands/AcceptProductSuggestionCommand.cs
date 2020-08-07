using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.ProductsSuggestions.Commands
{
    public class AcceptProductSuggestionCommand : ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}