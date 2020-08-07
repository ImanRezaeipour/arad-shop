using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Orders.Commands
{
    public class CustomerRejectOrderCommand : ShoppingCommandBase
    {
        public long OrderId { get; set; }
    }
}