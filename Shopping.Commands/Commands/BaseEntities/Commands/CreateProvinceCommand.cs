using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.BaseEntities.Commands
{
    public class CreateProvinceCommand : ShoppingCommandBase
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }
}