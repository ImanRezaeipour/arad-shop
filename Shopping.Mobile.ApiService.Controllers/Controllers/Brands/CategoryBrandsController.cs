using System;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Brands;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Brands
{
    public class CategoryBrandsController : ApiMobileControllerBase
    {
        private readonly IBrandQueryService _brandQueryService;
        public CategoryBrandsController(IBrandQueryService brandQueryService)
        {
            _brandQueryService = brandQueryService;
        }
        public IHttpActionResult Get(Guid categoryId)
        {
            var dto = new MobileResultDto
            {
                Result = _brandQueryService.GetActiveCategoryBrands(categoryId)
            };
            return Ok(dto);
        }
    }
}