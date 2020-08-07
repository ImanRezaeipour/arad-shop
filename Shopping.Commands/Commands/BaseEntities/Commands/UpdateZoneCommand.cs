using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.BaseEntities.Commands
{
    public class UpdateZoneCommand:ShoppingCommandBase
    {
        public long Id { get; set; }
        public Guid CityId { get; set; }
        public string Title { get; set; }
    }
}