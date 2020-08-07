using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Factors;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Factors
{
    public class LastFactorCommentController : ApiMobileControllerBase
    {
        private readonly IFactorQueryService _factorQueryService;
        public LastFactorCommentController(IFactorQueryService factorQueryService)
        {
            _factorQueryService = factorQueryService;
        }
        [Authorize(Roles = "Customer")]
        public IHttpActionResult Get()
        {
            var dto = new MobileResultDto
            {
                Result = _factorQueryService.GetLastFactorWithoutComment(UserId)
            };
            return Ok(dto);
        }
    }
}