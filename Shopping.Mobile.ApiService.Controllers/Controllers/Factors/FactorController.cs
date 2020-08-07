using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Factors;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Factors
{
    public class FactorController : ApiMobileControllerBase
    {
        private readonly IFactorQueryService _factorQueryService;
        public FactorController(IFactorQueryService factorQueryService)
        {
            _factorQueryService = factorQueryService;
        }
        [Authorize(Roles = "Customer,Shop")]
        public IHttpActionResult Get(long id)
        {
            var result = new MobileResultDto
            {
                Result = _factorQueryService.GetById(id)
            };
            return Ok(result);
        }
    }
}