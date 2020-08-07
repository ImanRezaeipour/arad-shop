using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Products.Commands
{
    public class DeleteProductCommand: ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}