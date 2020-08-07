using System.Linq;
using FluentValidation;
using Shopping.Commands.Commands.OrdersSuggestions.Commands;

namespace Shopping.Commands.Commands.OrdersSuggestions.Validators
{
    public class AcceptOrderSuggestionCommandValidator:AbstractValidator<AcceptOrderSuggestionCommand>
    {
        public AcceptOrderSuggestionCommandValidator()
        {
            RuleFor(item => item.AcceptOrderSuggestionItem).Must(s => s.Any())
                .WithMessage("برای تایید پیشنهاد حداقل یک محصول باید تایید شود");
        }
    }
}