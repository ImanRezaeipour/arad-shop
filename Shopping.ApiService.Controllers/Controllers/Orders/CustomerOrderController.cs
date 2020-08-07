using System;
using System.Web.Http;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Orders;

namespace Shopping.ApiService.Controllers.Controllers.Orders
{
    [Authorize(Roles = "Support,Admin")]
    public class CustomerOrderController : ApiControllerBase
    {
        private readonly IOrderQueryService _orderQueryService;
        public CustomerOrderController(IOrderQueryService orderQueryService)
        {
            _orderQueryService = orderQueryService;
        }
        [CustomQueryable]
        public IHttpActionResult Get(Guid customerId)
        {
            return Ok(_orderQueryService.GetCustomerOrders(customerId));
        }
    }
}