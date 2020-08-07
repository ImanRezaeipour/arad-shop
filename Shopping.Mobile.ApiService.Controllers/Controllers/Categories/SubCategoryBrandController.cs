using System;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Categories;

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Categories
{
    public class SubCategoryBrandController : ApiMobileControllerBase
    {
        private readonly ICategoryQueryService _categoryQueryService;
        public SubCategoryBrandController(ICategoryQueryService categoryQueryService)
        {
            _categoryQueryService = categoryQueryService;
        }
        public IHttpActionResult Get(Guid categoryId, Guid brandId)
        {
            var dto = new MobileResultDto
            {
                Result = _categoryQueryService.GetActiveBrandSubCategories(categoryId, brandId)
            };
            return Ok(dto);
        }
    }
}