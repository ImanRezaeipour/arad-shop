using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Factors;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Factors
{
    [Authorize(Roles = "Shop")]
    [RoutePrefix("api/ShopPaidNotSendFactor")]
    public class ShopPaidNotSendFactorController : ApiMobileControllerBase
    {
        private readonly IFactorQueryService _factorQueryService;
        public ShopPaidNotSendFactorController(IFactorQueryService factorQueryService)
        {
            _factorQueryService = factorQueryService;
        }

        public IHttpActionResult Get([FromUri]PagedInputDto pagedInput)
        {
            return Ok(_factorQueryService.GetShopPaidNotSendFactors(UserId, pagedInput));
        }

        [Route("Count")]
        public IHttpActionResult Get()
        {
            var dto = new MobileResultDto
            {
                Result = _factorQueryService.GetShopPaidNotSendFactorsCount(UserId)
            };
            return Ok(dto);
        }
    }
}