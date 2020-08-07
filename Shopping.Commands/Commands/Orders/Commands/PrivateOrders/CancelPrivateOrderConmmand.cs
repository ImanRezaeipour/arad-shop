using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Orders.Commands.PrivateOrders
{
    public class CancelPrivateOrderConmmand : ShoppingCommandBase
    {
        public long OrderId { get; set; }
    }
}