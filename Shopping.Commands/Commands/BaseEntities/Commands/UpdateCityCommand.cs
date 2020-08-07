using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.BaseEntities.Commands
{
    public class UpdateCityCommand : ShoppingCommandBase
    {
        public Guid Id { get; set; }
        public string CityName { get; set; }
        public string Code { get; set; }
    }
}