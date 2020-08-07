using System;
using System.Collections.Generic;
using FluentValidation.Attributes;
using Shopping.Commands.Commands.OrdersSuggestions.Validators;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.OrdersSuggestions.Commands
{
    [Validator(typeof(AcceptOrderSuggestionCommandValidator))]
    public class AcceptOrderSuggestionCommand:ShoppingCommandBase
    {
        public Guid OrderSuggestionId { get; set; }
        public List<AcceptOrderSuggestionItemCommand> AcceptOrderSuggestionItem { get; set; }
    }
}