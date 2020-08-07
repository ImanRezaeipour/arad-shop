using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Accounting;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Accounting
{
    [Authorize(Roles = "Shop")]
    public class ShopSettlementDetailController : ApiMobileControllerBase
    {
        private readonly IAccountingQueryService _accountingQueryService;
        public ShopSettlementDetailController(IAccountingQueryService accountingQueryService)
        {
            _accountingQueryService = accountingQueryService;
        }
        public IHttpActionResult Get(string fromDate, string toDate)
        {
            var dto = new MobileResultDto
            {
                Result = _accountingQueryService.GetShopSettlement(UserId, fromDate, toDate)
            };
            return Ok(dto);
        }
    }
}