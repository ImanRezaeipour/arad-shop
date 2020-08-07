using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Persons.Commands.Shop
{
    public class RejectShopCommand : ShoppingCommandBase
    {
        public Guid Id { get; set; }
        public string DescriptionStatus { get; set; }
    }
}