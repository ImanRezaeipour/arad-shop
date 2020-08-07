using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.ProductsSuggestions.Commands
{
    public class RejectProductSuggestionCommand : ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}