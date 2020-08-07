using System.Web.Http;
using Shopping.QueryService.Interfaces.Marketers;

namespace Shopping.ApiService.Controllers.Controllers.Marketers
{
    public class MarketerShopFactorWeeklyChartController:ApiControllerBase
    {
        private readonly IMarketerQueryService _marketerQueryService;

        public MarketerShopFactorWeeklyChartController(IMarketerQueryService marketerQueryService)
        {
            _marketerQueryService = marketerQueryService;
        }
        public IHttpActionResult Get(long marketerId) 
        {
            return Ok(_marketerQueryService.GetMarketerShopWeeklyCharts(marketerId));
        }
    }
}