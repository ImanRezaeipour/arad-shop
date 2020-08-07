using System;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Products;
#pragma warning disable 1591

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Products
{
    public class BrandCategoryProductController : ApiMobileControllerBase
    {
        private readonly IProductQueryService _productQueryService;
        public BrandCategoryProductController(IProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }
        public IHttpActionResult Get(Guid categoryId, Guid brandId, [FromUri]PagedInputDto pagedInput)
        {
            return Ok(_productQueryService.GetProductsWithCategoryAndBrand(categoryId, brandId, pagedInput));
        }
    }
}