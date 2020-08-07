using System.Linq;
using FluentValidation;
using Shopping.Commands.Commands.Orders.Commands.PrivateOrder;

namespace Shopping.Commands.Commands.Orders.Validators
{
    public class CreatePrivateOrderCommandValidator : AbstractValidator<CreatePrivateOrderCommand>
    {
        public CreatePrivateOrderCommandValidator()
        {
            RuleFor(item => item.OrderItems).Must(p => p.Any(i => i.Quantity > 0))
                .WithMessage("تعداد اقلام سفارش باید بیشتر از صفر باشد");
        }
    }
}