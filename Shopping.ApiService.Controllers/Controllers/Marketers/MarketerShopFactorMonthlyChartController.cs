using System.Web.Http;
using Shopping.QueryService.Interfaces.Marketers;

namespace Shopping.ApiService.Controllers.Controllers.Marketers
{
    public class MarketerShopFactorMonthlyChartController:ApiControllerBase
    {
        private readonly IMarketerQueryService _marketerQueryService;

        public MarketerShopFactorMonthlyChartController(IMarketerQueryService marketerQueryService)
        {
            _marketerQueryService = marketerQueryService;
        }

        public IHttpActionResult Get(long marketerId)
        {
            return Ok(_marketerQueryService.GetMarketerShopMonthlyCharts(marketerId));
        }
    }
}