using System.Web.Http;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Discounts;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Discounts
{
    public class TodayHavePercentDiscountController : ApiMobileControllerBase
    {
        private readonly IPercentDiscountQueryService _percentDiscountQueryService;
        public TodayHavePercentDiscountController(IPercentDiscountQueryService percentDiscountQueryService)
        {
            _percentDiscountQueryService = percentDiscountQueryService;
        }
        public IHttpActionResult Get()
        {
            return Ok(_percentDiscountQueryService.CheckUserTodayHavePercentDiscount(UserId));
        }
    }
}