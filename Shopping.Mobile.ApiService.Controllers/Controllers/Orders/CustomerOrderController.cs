using System;
using System.Web.Http;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Orders;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Orders
{
    public class CustomerOrderController : ApiMobileControllerBase
    {
        private readonly IOrderQueryService _orderQueryService;
        public CustomerOrderController(ICommandBus bus, IOrderQueryService orderQueryService) : base(bus)
        {
            _orderQueryService = orderQueryService;
        }
        /// <summary>
        /// دریافت لیست سفارش های مشتری
        /// </summary>
        /// <returns></returns>
        //[Authorize(Roles = "Customer")]
        public IHttpActionResult Get([FromUri]PagedInputDto pagedInput)
        {
            return Ok(_orderQueryService.GetCustomerOrdersByUserId(UserId, pagedInput));
        }
    }
}