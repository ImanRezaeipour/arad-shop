using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Sliders.Commands
{
    public class DeActiveSliderCommand: ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}