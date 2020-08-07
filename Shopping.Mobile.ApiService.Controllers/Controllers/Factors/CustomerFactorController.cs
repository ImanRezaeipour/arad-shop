using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Factors;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Factors
{
    public class CustomerFactorController : ApiMobileControllerBase
    {
        private readonly IFactorQueryService _factorQueryService;
        public CustomerFactorController(IFactorQueryService factorQueryService)
        {
            _factorQueryService = factorQueryService;
        }
         [Authorize(Roles = "Customer")]
        public IHttpActionResult Get([FromUri]PagedInputDto pagedInput)
        {
            return Ok(_factorQueryService.GetCustomerFactors(UserId, pagedInput));
        }
    }
}