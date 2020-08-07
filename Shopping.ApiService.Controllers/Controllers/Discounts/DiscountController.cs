using System.Web.Http;
using Shopping.Infrastructure.OData;
using Shopping.QueryService.Interfaces.Discounts;

namespace Shopping.ApiService.Controllers.Controllers.Discounts
{
    public class DiscountController:ApiControllerBase
    {
        private readonly IPercentDiscountQueryService _discountQueryService;
        public DiscountController(IPercentDiscountQueryService discountQueryService)
        {
            _discountQueryService = discountQueryService;
        }
        [CustomQueryable]
        public IHttpActionResult Get()
        {
            return Ok(_discountQueryService.GetAll());
        }
    }
}