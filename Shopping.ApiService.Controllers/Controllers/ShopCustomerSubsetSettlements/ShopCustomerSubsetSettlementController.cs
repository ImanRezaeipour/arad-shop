using System;
using System.Web.Http;
using Shopping.QueryService.Interfaces.ShopCustomerSubsetSettlements;

namespace Shopping.ApiService.Controllers.Controllers.ShopCustomerSubsetSettlements
{
    [Authorize(Roles = "Support,Admin")]
    public class ShopCustomerSubsetSettlementController : ApiControllerBase
    {
        private readonly IShopCustomerSubsetSettlementQueryService _shopCustomerSubsetSettlementQueryService;
        public ShopCustomerSubsetSettlementController(IShopCustomerSubsetSettlementQueryService shopCustomerSubsetSettlementQueryService)
        {
            _shopCustomerSubsetSettlementQueryService = shopCustomerSubsetSettlementQueryService;
        }
        public IHttpActionResult Get(Guid shopId)
        {
            var result = _shopCustomerSubsetSettlementQueryService.GetByShopId(shopId);
            return Ok(result);
        }
    }
}