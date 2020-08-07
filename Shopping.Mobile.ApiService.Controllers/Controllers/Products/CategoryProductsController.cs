using System;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Products;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Products
{
    public class CategoryProductsController : ApiMobileControllerBase
    {
        private readonly IProductQueryService _productQueryService;
        public CategoryProductsController(IProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }
        public IHttpActionResult Get(Guid categoryId, [FromUri]PagedInputDto pagedInput)
        {
            return Ok(_productQueryService.GetCategoryProducts(categoryId, pagedInput));
        }
    }
}