using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.BaseEntities.Commands
{
    public class ActiveCityCommand:ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}