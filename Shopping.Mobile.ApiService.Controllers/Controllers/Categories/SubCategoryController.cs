using System;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Categories;
#pragma warning disable 1591

namespace Shopping.Mobile.ApiService.Controllers.Controllers.Categories
{
    public class SubCategoryController : ApiMobileControllerBase
    {
        private readonly ICategoryQueryService _categoryQueryService;
        public SubCategoryController(ICategoryQueryService categoryQueryService)
        {
            _categoryQueryService = categoryQueryService;
        }
        public IHttpActionResult Get(Guid categoryId)
        {
            var dto = new MobileResultDto
            {
                Result = _categoryQueryService.GetActiveSubCategories(categoryId)
            };
            return Ok(dto);
        }
    }
}