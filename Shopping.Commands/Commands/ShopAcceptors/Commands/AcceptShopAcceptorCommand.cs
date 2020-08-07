using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.ShopAcceptors.Commands
{
    public class AcceptShopAcceptorCommand:ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}