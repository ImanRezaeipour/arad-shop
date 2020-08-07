using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Brands.Commands
{
    public class DeleteBrandCommand:ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}