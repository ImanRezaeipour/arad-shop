using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.Payments.Responses
{
    public class PaymentFactorCommandResponse : ShoppingCommandResponseBase
    {
        public PaymentFactorCommandResponse(string ipgUrl)
        {
            IpgUrl = ipgUrl;
        }
        public string IpgUrl { get; private set; }
    }
}