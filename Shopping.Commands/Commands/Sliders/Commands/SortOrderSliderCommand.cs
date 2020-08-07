using System;
using System.Collections.Generic;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Sliders.Commands
{
    public class SortOrderSliderCommand : ShoppingCommandBase
    {
        public List<SliderSortItem> Sliders { get; set; }
    }

    public class SliderSortItem
    {
        public Guid Id { get; set; }
        public int Order { get; set; }
    }
}