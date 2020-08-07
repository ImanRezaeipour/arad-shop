using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.QueryService.Interfaces.Persons;

namespace Shopping.ApiService.Controllers.Controllers.Shops.CustomerSubsets
{
    public class ShopCustomerSubsetReportController : ApiControllerBase
    {
        private readonly IShopQueryService _shopQueryService;
        public ShopCustomerSubsetReportController(IShopQueryService shopQueryService)
        {
            _shopQueryService = shopQueryService;
        }
        public async Task<IHttpActionResult> Get(Guid shopId)
        {
            var result = await _shopQueryService.GetShopCustomerSubsetReport(shopId);
            return Ok(result);
        }
    }
}