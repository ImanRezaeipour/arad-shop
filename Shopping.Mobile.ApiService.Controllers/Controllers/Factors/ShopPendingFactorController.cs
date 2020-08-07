using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Factors;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Factors
{
    public class ShopPendingFactorController : ApiMobileControllerBase
    {
        private readonly IFactorQueryService _factorQueryService;
        public ShopPendingFactorController(IFactorQueryService factorQueryService)
        {
            _factorQueryService = factorQueryService;
        }
        [Authorize(Roles = "Shop")]
        public IHttpActionResult Get([FromUri]PagedInputDto pagedInput)
        {
            return Ok(_factorQueryService.GetPendingShopFactors(UserId, pagedInput));
        }
    }
}