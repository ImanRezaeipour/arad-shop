using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Products;
#pragma warning disable 1591

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Products
{
    public class ProductFilterByNameController : ApiMobileControllerBase
    {
        private readonly IProductQueryService _productQueryService;
        public ProductFilterByNameController(IProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }
        public IHttpActionResult Get(string text, [FromUri]PagedInputDto pagedInput)
        {
            text = text.Replace("ی", "ی");
            return Ok(_productQueryService.GetProductsWithNameAndBrandName(text, pagedInput));
        }
    }
}