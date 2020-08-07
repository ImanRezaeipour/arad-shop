using System.Linq;
using FluentValidation;
using Shopping.Commands.Commands.Orders.Commands.AreaOrder;

namespace Shopping.Commands.Commands.Orders.Validators
{
    public class CreateAreaOrderCommandValidator : AbstractValidator<CreateAreaOrderCommand>
    {
        public CreateAreaOrderCommandValidator()
        {
            RuleFor(item => item.OrderItems).Must(p => p.Any(i => i.Quantity > 0))
                .WithMessage("تعداد اقلام سفارش باید بیشتر از صفر باشد");
        }
    }
}