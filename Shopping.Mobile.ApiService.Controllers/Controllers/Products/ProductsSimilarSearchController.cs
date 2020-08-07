using System;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Products;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Products
{
    public class ProductsSimilarSearchController : ApiMobileControllerBase
    {
        private readonly IProductQueryService _productQueryService;
        public ProductsSimilarSearchController(IProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }
        public IHttpActionResult Get(Guid productId, string name, [FromUri]PagedInputDto pagedInput)
        {
            return Ok(_productQueryService.GetCategoryProductsSimilarWithName(productId, name, pagedInput));
        }
    }
}