using System;
using Shopping.Commands.Commands.Shared;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Discounts.Commands
{
    public class AddProductToPercentDiscountCommand: ShoppingCommandBase
    {
        public Guid PercentDiscount { get; set; }
        public Guid ProductId { get; set; }
        public UserInfoCommandItem UserInfoCommand { get; set; }
    }
}