using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Sliders.Commands
{
    public class ChangeTypeSliderToDiscountCommand : ShoppingCommandBase
    {
        public string ImageName { get; set; }
        public SliderType SliderType { get; set; }
    }
}