using System.Web.Http;
using Shopping.QueryService.Interfaces.Marketers;

namespace Shopping.ApiService.Controllers.Controllers.Marketers
{
    public class MarketerShopFactorYearlyChartController:ApiControllerBase
    {
        private readonly IMarketerQueryService _marketerQueryService;

        public MarketerShopFactorYearlyChartController(IMarketerQueryService marketerQueryService)
        {
            _marketerQueryService = marketerQueryService;
        }
        public IHttpActionResult Get(long marketerId)
        {
            return Ok(_marketerQueryService.GetMarketerShopYearlyCharts(marketerId));
        }
    }
}