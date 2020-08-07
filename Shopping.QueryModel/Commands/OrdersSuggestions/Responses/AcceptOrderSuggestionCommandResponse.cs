using Shopping.Infrastructure.SeedWorks;

namespace Shopping.Commands.Commands.OrdersSuggestions.Responses
{
    public class AcceptOrderSuggestionCommandResponse:ShoppingCommandResponseBase
    {
        public AcceptOrderSuggestionCommandResponse(long factorId)
        {
            FactorId = factorId;
        }

        public long FactorId { get;private set; }
    }
}