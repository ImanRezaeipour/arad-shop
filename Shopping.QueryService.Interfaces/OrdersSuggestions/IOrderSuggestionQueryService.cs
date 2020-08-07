using System.Collections.Generic;
using System.Threading.Tasks;
using Shopping.QueryModel.QueryModels.OrdersSuggestions;

namespace Shopping.QueryService.Interfaces.OrdersSuggestions
{
    public interface IOrderSuggestionQueryService
    {
        IOrderSuggestionDto GetByOrderId(long orderId);
        Task<IOrderSuggestionDto> GetAcceptedOrderSuggestion(long orderId);
    }
}