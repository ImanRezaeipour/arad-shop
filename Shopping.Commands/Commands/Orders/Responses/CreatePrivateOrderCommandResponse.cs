using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Orders.Responses
{
    public class CreatePrivateOrderCommandResponse : ShoppingCommandResponseBase
    {
        public CreatePrivateOrderCommandResponse(long orderId, int expireOrderTime)
        {
            OrderId = orderId;
            ExpireOrderTime = expireOrderTime;
        }
        public long OrderId { get; private set; }
        /// <summary>
        /// ثانیه
        /// </summary>
        public int ExpireOrderTime { get; private set; }
    }
}