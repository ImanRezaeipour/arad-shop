using System.Web.Http;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Persons;

namespace Shopping.ApiService.Controllers.Controllers.Shops
{
    public class SellingShopController:ApiControllerBase
    {
        private readonly IShopQueryService _shopQueryService;

        public SellingShopController(IShopQueryService shopQueryService)
        {
            _shopQueryService = shopQueryService;
        }
        [CustomQueryable]
        public IHttpActionResult Get(int month, long marketerId)
        {
            return Ok(_shopQueryService.IsHaveSellingShop(month, marketerId));
        }
    }
}