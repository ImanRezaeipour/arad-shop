using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Discounts.Commands
{
    public class DeleteProductFromPercentDiscountCommand:ShoppingCommandBase
    {
        public Guid PercentDiscount { get; set; }
        public Guid ProductId { get; set; }
    }
}