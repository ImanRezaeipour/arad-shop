using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Sliders.Commands
{
    public class CreateSliderCommand:ShoppingCommandBase
    {
        public string ImageName { get; set; }
    }
}