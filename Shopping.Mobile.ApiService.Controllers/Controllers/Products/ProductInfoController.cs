using System;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Products;

#pragma warning disable 1591
namespace Shopping.Mobile.ApiService.Controllers.Controllers.Products
{
    public class ProductInfoController : ApiMobileControllerBase
    {
        private readonly IProductQueryService _productQueryService;
        public ProductInfoController(IProductQueryService productQueryService)
        {
            _productQueryService = productQueryService;
        }
        /// <summary>
        /// دریافت یک محصول
        /// </summary>
        /// <param name="productId"></param>
        /// <returns></returns>
        public IHttpActionResult Get(Guid productId)
        {
            var dto = new MobileResultDto
            {
                Result = _productQueryService.GetById(productId)
            };
            return Ok(dto);
        }
    }
}