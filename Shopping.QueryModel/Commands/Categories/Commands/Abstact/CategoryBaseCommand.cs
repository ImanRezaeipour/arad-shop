using System;
using Shopping.Commands.Commands.Categories.Commands.Items;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Categories.Commands.Abstact
{
    public abstract class CategoryBaseCommand : ShoppingCommandBase
    {
        public Guid Id => Guid.NewGuid();
        public string Name { get; set; }
        public string Description { get; set; }
        public CategoryImageCommand CategoryImage { get; set; }
    }
}