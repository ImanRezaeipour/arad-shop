using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Orders.Commands.PrivateOrders
{
    public class ConvertPrivateToAreaOrderCommand: ShoppingCommandBase
    {
        public long OrderId { get; set; }
    }
}