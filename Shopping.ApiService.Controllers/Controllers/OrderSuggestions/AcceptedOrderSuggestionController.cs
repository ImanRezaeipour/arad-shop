using System.Threading.Tasks;
using System.Web.Http;
using Shopping.QueryService.Interfaces.OrdersSuggestions;

namespace Shopping.ApiService.Controllers.Controllers.OrderSuggestions
{
    public class AcceptedOrderSuggestionController : ApiControllerBase
    {
        private readonly IOrderSuggestionQueryService _orderSuggestionQueryService;
        public AcceptedOrderSuggestionController(IOrderSuggestionQueryService orderSuggestionQueryService)
        {
            _orderSuggestionQueryService = orderSuggestionQueryService;
        }
        public async Task<IHttpActionResult> Get(long orderId)
        {
            var result = await _orderSuggestionQueryService.GetAcceptedOrderSuggestion(orderId);
            return Ok(result);
        }
    }
}