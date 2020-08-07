using System;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.BaseEntities.Commands
{
    public class DeActiveCityCommand:ShoppingCommandBase
    {
        public Guid Id { get; set; }
    }
}