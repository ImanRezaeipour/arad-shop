using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Sliders.Commands
{
    public class DeleteSlideCommand:ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}