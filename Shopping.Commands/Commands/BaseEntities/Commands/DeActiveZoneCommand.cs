using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.BaseEntities.Commands
{
    public class DeActiveZoneCommand:ShoppingCommandBase
    {
        public long Id { get; set; }
        public Guid CityId { get; set; }

        
    }
}