//using System;
//using System.Web.Http;
//using Shopping.QueryService.Interfaces.ShopOrderLogs;

//namespace Shopping.ApiService.Controllers.Controllers.ShopOrderLogs
//{
//    [Authorize(Roles = "Admin,Support")]
//    public class ShopOrderLogController:ApiControllerBase
//    {
//        private readonly IShopOrderLogQueryService _orderLogQueryService;
//        public ShopOrderLogController(IShopOrderLogQueryService orderLogQueryService)
//        {
//            _orderLogQueryService = orderLogQueryService;
//        }
//        public IHttpActionResult Get(Guid id)
//        {
//            return Ok(_orderLogQueryService.GetShopOrderLogByShopId(id));
//        }
//    }
//}