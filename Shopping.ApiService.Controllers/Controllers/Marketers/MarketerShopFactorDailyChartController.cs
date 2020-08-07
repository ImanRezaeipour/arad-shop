using System.Web.Http;
using Shopping.QueryService.Interfaces.Marketers;

namespace Shopping.ApiService.Controllers.Controllers.Marketers
{
    public class MarketerShopFactorDailyChartController:ApiControllerBase
    {
        private readonly IMarketerQueryService _marketerQueryService;

        public MarketerShopFactorDailyChartController(IMarketerQueryService marketerQueryService)
        {
            _marketerQueryService = marketerQueryService;
        }
        public IHttpActionResult Get(long marketerId)
        {
            return Ok(_marketerQueryService.GetMarketerShopDailyCharts(marketerId));
        }
    }
}