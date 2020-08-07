using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.BaseEntities.Commands
{
    public class CreateZoneCommand:ShoppingCommandBase
    {
        public Guid CityId { get; set; }
        public string Title { get; set; }
    }
}