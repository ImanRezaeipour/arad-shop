using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Categories.Commands
{
    public class DeleteCategoryRootCommand: ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}