using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Factors;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Factors
{
    public class ShopFactorController : ApiMobileControllerBase
    {
        private readonly IFactorQueryService _factorQueryService;
        public ShopFactorController(IFactorQueryService factorQueryService)
        {
            _factorQueryService = factorQueryService;
        }
        public async Task<IHttpActionResult> Get([FromUri] PagedInputDto pagedInput)
        {
            var factors = await _factorQueryService.GetShopFactors(UserId, pagedInput);
            return Ok(factors);
        }
    }
}