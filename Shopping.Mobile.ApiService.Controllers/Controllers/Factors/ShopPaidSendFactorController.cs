using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Factors;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Factors
{
    [Authorize(Roles = "Shop")]
    [RoutePrefix("api/ShopPaidSendFactor")]
    public class ShopPaidSendFactorController : ApiMobileControllerBase
    {
        private readonly IFactorQueryService _factorQueryService;
        public ShopPaidSendFactorController(IFactorQueryService factorQueryService)
        {
            _factorQueryService = factorQueryService;
        }
        public IHttpActionResult Get([FromUri]PagedInputDto pagedInput)
        {
            return Ok(_factorQueryService.GetShopPaidSendFactors(UserId, pagedInput));
        }
        [Route("Count")]
        public IHttpActionResult Get()
        {
            var dto = new MobileResultDto
            {
                Result = _factorQueryService.GetShopPaidSendFactorsCount(UserId)
            };
            return Ok(dto);
        }
    }
}