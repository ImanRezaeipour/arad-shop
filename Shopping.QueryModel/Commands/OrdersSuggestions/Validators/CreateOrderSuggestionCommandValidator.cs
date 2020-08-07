using System.Linq;
using FluentValidation;
using Shopping.Commands.Commands.OrdersSuggestions.Commands;

namespace Shopping.Commands.Commands.OrdersSuggestions.Validators
{
    public class CreateOrderSuggestionCommandValidator : AbstractValidator<CreateOrderSuggestionCommand>
    {
        public CreateOrderSuggestionCommandValidator()
        {
            RuleFor(item => item.OrderSuggestionItems).Must(p => p.Any(i => i.Price > 0))
                .WithMessage("مبالغ اقلام باید بیشتر از صفر باشد");
            RuleFor(item => item.OrderSuggestionItems).Must(p => p.Any(i => i.Quantity > 0))
                .WithMessage("تعداد هر یک از اقلام باید بیشتر از صفر باشد");
            RuleFor(item => item.ShippingTime).GreaterThan(0).
                WithMessage("زمان ارسال سفارش باید بیشتر از صفر باشد");
        }
    }
}