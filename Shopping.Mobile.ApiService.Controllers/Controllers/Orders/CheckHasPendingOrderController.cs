using System;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Orders;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Orders
{
    public class CheckHasPendingOrderController:ApiMobileControllerBase
    {
        private readonly IOrderQueryService _orderQueryService;
        public CheckHasPendingOrderController(IOrderQueryService orderQueryService)
        {
            _orderQueryService = orderQueryService;
        }
      
        public  IHttpActionResult Get()
        {
            return Ok(new MobileResultDto
            {
                Result =  _orderQueryService.CheckHasPendingOrder(UserId)
            });
        }
    }
}