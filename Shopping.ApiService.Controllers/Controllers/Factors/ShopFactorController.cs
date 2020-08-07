using System;
using System.Web.Http;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Factors;

namespace Shopping.ApiService.Controllers.Controllers.Factors
{
    public class ShopFactorController : ApiControllerBase
    {
        private readonly IFactorQueryService _factorQueryService;
        public ShopFactorController(IFactorQueryService factorQueryService)
        {
            _factorQueryService = factorQueryService;
        }
        [CustomQueryable]
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get(Guid shopId)
        {
            return Ok(_factorQueryService.GetFactorsByShopId(shopId));
        }
    }
}