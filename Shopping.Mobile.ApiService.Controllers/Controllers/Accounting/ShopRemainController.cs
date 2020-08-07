using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Accounting;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Accounting
{
    [Authorize(Roles = "Shop")]
    public class ShopRemainController : ApiMobileControllerBase
    {
        private readonly IAccountingQueryService _accountingQueryService;
        public ShopRemainController(IAccountingQueryService accountingQueryService)
        {
            _accountingQueryService = accountingQueryService; 
        } 
        public IHttpActionResult Get(string fromDate, string toDate)
        {
            var dto = new MobileResultDto
            {
                Result = _accountingQueryService.GetShopRemain(UserId, fromDate, toDate)
            };
            return Ok(dto);
        }
    }
}