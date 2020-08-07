using System;
using System.Web.Http;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Discounts;

namespace Shopping.ApiService.Controllers.Controllers.Discounts
{
    public class DiscountPercentShopSellController : ApiControllerBase
    {
        private readonly IPercentDiscountQueryService _discountQueryService;
        public DiscountPercentShopSellController(IPercentDiscountQueryService discountQueryService)
        {
            _discountQueryService = discountQueryService;
        }
        [CustomQueryable]
        public IHttpActionResult Get(Guid percentDiscountId)
        {
            return Ok(_discountQueryService.GetPercentDiscountShopSells(percentDiscountId));
        }
    }
}