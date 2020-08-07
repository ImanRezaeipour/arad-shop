using System;
using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Sliders.Commands
{
    public class AddSliderToDiscountCommand:ShoppingCommandBase
    {
        public string ImageName  { get; set; }
        public Guid DiscountId { get; set; }
        public SliderType SliderType { get; set; }
    }
}