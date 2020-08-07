using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.QueryService.Interfaces.ShopMarketersHistories;

namespace Shopping.ApiService.Controllers.Controllers.ShopMarketersHistories
{
    public class ShopMarketersHistoryController : ApiControllerBase
    {
        private readonly IShopMarketersHistoryQueryService _shopMarketersHistoryQueryService;
        public ShopMarketersHistoryController(IShopMarketersHistoryQueryService shopMarketersHistoryQueryService)
        {
            _shopMarketersHistoryQueryService = shopMarketersHistoryQueryService;
        }
        public async Task<IHttpActionResult> Get(Guid shopId)
        {
            return Ok(await _shopMarketersHistoryQueryService.GetShopMarketers(shopId));
        }
    }
}