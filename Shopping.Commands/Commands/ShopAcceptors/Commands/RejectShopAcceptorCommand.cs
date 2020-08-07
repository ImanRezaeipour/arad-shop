using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.ShopAcceptors.Commands
{
    public class RejectShopAcceptorCommand:ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}