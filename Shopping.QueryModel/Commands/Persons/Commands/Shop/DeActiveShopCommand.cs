using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Persons.Commands.Shop
{
    public class DeActiveShopCommand:ShoppingCommandBase
    {
        public Guid Id { get; set; }   
    }
}