using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.BaseEntities.Commands
{
    public class ActiveZoneCommand:ShoppingCommandBase
    {
        public Guid CityId { get; set; }
        public long Id { get; set; }
    }
}