using System;
using System.Web.Http;
using Shopping.QueryService.Interfaces.Persons;

namespace Shopping.ApiService.Controllers.Controllers.Shops
{
    public class ShopFactorDailyChartController:ApiControllerBase
    {
        private readonly IShopQueryService _shopQueryService;

        public ShopFactorDailyChartController(IShopQueryService shopQueryService)
        {
            _shopQueryService = shopQueryService;
        }
        public IHttpActionResult Get(Guid shopId)
        {
            return Ok(_shopQueryService.GetShopDailyCharts(shopId));
        }
    }
}