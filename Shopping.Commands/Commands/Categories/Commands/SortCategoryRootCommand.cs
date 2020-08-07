using System.Collections.Generic;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Categories.Commands
{
    public class SortCategoryRootCommand: ShoppingCommandBase
    {
        public List<CategoryOrderCommand> CategoryOrders { get; set; }
    }
}