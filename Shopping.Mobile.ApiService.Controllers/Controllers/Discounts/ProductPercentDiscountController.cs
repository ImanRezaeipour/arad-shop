using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Discounts;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Discounts
{
    public class ProductPercentDiscountController : ApiMobileControllerBase
    {
        private readonly IPercentDiscountQueryService _percentDiscountQueryService;
        public ProductPercentDiscountController(IPercentDiscountQueryService percentDiscountQueryService)
        {
            _percentDiscountQueryService = percentDiscountQueryService;
        }
        public async Task<IHttpActionResult> Get(Guid percentDiscountId, [FromUri]PagedInputDto pagedInput)
        {
            var result =
                await _percentDiscountQueryService.GetProductPercentDiscountByIdPaging(percentDiscountId, pagedInput);
            return Ok(result);
        }
    }
}