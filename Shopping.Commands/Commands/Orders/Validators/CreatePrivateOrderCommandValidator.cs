using System.Linq;
using FluentValidation;
using Shopping.Commands.Commands.Orders.Commands.PrivateOrders;

namespace Shopping.Commands.Commands.Orders.Validators
{
    public class CreatePrivateOrderCommandValidator : AbstractValidator<CreatePrivateOrderCommand>
    {
        public CreatePrivateOrderCommandValidator()
        {
            RuleFor(item => item.OrderItems).Must(p => p.Count(item => item.IsPercentDiscount) <= 1)
                .WithMessage("شما مجاز به انتخاب یک کالای تخفیف دار در هر سفارش می باشید");
        }
    }
}