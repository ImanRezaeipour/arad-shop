using System;
using System.Web.Http;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Orders;

namespace Shopping.ApiService.Controllers.Controllers.Orders
{
    [Authorize(Roles = "Admin,Support")]
    public class ShopOrderController : ApiControllerBase
    {
        private readonly IOrderQueryService _orderQueryService;
        public ShopOrderController(IOrderQueryService orderQueryService)
        {
            _orderQueryService = orderQueryService;
        }
        [CustomQueryable]
        public IHttpActionResult Get(Guid shopId)
        {
            var result = _orderQueryService.GetShopOrders(shopId);
            return Ok(result);
        }
    }
}