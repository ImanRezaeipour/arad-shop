using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Orders.Responses
{
    public class ConvertPrivateToAreaOrderCommandResponse: ShoppingCommandResponseBase
    {
        public ConvertPrivateToAreaOrderCommandResponse(int expireOrderTime)
        {
            ExpireOrderTime = expireOrderTime;
        }
        public int ExpireOrderTime { get; private set; }
    }
}