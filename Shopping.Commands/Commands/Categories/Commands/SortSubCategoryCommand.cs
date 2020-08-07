using System;
using System.Collections.Generic;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Categories.Commands
{
    public class SortSubCategoryCommand: ShoppingCommandBase
    {
        public Guid CaegoryRootId { get; set; }
        public List<CategoryOrderCommand> CategoryOrders { get; set; }
    }
}