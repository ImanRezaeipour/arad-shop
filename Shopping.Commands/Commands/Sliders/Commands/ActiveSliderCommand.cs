using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Sliders.Commands
{
    public class ActiveSliderCommand:ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}