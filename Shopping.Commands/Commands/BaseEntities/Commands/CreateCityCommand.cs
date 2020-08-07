using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.BaseEntities.Commands
{
    public class CreateCityCommand:ShoppingCommandBase
    {
        public string CityName { get; set; }
        public Guid ProvinceId { get; set; }
        public string Code { get; set; }
    }
}