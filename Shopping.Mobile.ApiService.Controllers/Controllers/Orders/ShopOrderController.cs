using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Orders;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Orders
{
    [RoutePrefix("api/ShopOrder")]
    public class ShopOrderController : ApiMobileControllerBase
    {
        private readonly IOrderQueryService _orderQueryService;
        public ShopOrderController(IOrderQueryService orderQueryService)
        {
            _orderQueryService = orderQueryService;
        }
        
        [Authorize(Roles = "Shop")]
        public async Task<IHttpActionResult> Get([FromUri]PagedInputDto pagedInput)
        {
            return Ok(await _orderQueryService.GetPendingShopOrders(UserId, pagedInput));
        }

        [Authorize(Roles = "Shop")]
        [Route("Count")]
        public IHttpActionResult GetOrderCount()
        {
            var dto = new MobileResultDto
            {
                Result = _orderQueryService.GetPendingShopOrdersCount(UserId)
            };
            return Ok(dto);
        }
    }
}