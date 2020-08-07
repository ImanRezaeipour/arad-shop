using System;
using System.Web.Http;
using Shopping.QueryService.Interfaces.Discounts;

namespace Shopping.ApiService.Controllers.Controllers.Discounts
{
    public class PercentDiscountSumReportController:ApiControllerBase
    {
        private readonly IPercentDiscountQueryService _discountQueryService;
        public PercentDiscountSumReportController(IPercentDiscountQueryService discountQueryService)
        {
            _discountQueryService = discountQueryService;
        }
        public IHttpActionResult Get(Guid percentDiscountId)
        {
            var result =  _discountQueryService.SumOfReport(percentDiscountId);
            return Ok(result);
        }
    }
}