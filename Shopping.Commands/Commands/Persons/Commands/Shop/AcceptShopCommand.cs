using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Persons.Commands.Shop
{
    public class AcceptShopCommand : ShoppingCommandBase
    {
        public Guid Id { get; set; }
        public string DescriptionStatus { get; set; }
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

    }
}