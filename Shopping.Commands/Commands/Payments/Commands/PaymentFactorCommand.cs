using Shopping.Infrastructure.Enum;
using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Payments.Commands
{
    public class PaymentFactorCommand : ShoppingCommandBase
    {
        public long FactorId { get; set; }
        public GatewayIpg Gateway { get; set; }
    }
}