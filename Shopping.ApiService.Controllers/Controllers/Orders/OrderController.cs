using System.Web.Http;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Orders;

namespace Shopping.ApiService.Controllers.Controllers.Orders
{
    public class OrderController : ApiControllerBase
    {
        private readonly IOrderQueryService _orderQueryService;
        public OrderController(IOrderQueryService orderQueryService)
        {
            _orderQueryService = orderQueryService;
        }
        /// <summary>
        /// دریافت لیست سفارشات مشتری
        /// </summary>
        /// <returns></returns>
        [CustomQueryable]
        //[Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get()
        {
            return Ok(_orderQueryService.GetOrders());
        }
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get(long id)
        {
            return Ok(_orderQueryService.Get(id));
        }
    }
}