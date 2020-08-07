using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Products;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Products
{
    public class OtherBrandCategoryProductController : ApiMobileControllerBase
    {
        private readonly IProductQueryService _productQueryService;
        public OtherBrandCategoryProductController(IProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }
        public async Task<IHttpActionResult> Get(Guid categoryId, Guid brandId, [FromUri] PagedInputDto pagedInput)
        {
            var result =
                await _productQueryService.GetProductsWithCategoryAndOtherBrand(categoryId, brandId, pagedInput);
            return Ok(result);
        }
    }
}