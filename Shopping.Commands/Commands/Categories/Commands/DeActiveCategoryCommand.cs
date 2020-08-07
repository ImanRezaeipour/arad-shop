using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Categories.Commands
{
    public class DeActiveCategoryCommand : ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}