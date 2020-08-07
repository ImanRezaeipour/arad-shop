using System;
using System.Web.Http;
using Shopping.QueryService.Interfaces.Persons;

namespace Shopping.ApiService.Controllers.Controllers.Shops
{
    public class ShopFactorMonthlyChartController:ApiControllerBase
    {
        private readonly IShopQueryService _shopQueryService;

        public ShopFactorMonthlyChartController(IShopQueryService shopQueryService)
        {
            _shopQueryService = shopQueryService;
        }
        public IHttpActionResult Get(Guid shopId)
        {
            return Ok(_shopQueryService.GetShopMonthlyCharts(shopId));
        }
    }
}