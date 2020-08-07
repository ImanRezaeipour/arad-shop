using System.Web.Http;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Orders;

namespace Shopping.ApiService.Controllers.Controllers.Orders
{
    public class AreaOrderController:ApiControllerBase
    {
        private readonly IOrderQueryService _orderQueryService;

        public AreaOrderController(IOrderQueryService orderQueryService)
        {
            _orderQueryService = orderQueryService;
        }
        [CustomQueryable]
        //[Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get(long privateOrderId)
        {
            return Ok(_orderQueryService.GetAreaOrderWithPrivateOrder(privateOrderId));
        }
    }
}