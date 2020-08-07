using System;
using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Categories;
#pragma warning disable 1591
namespace Shopping.Mobile.ApiService.Controllers.Controllers.Categories
{
    public class CategoryWithProductController : ApiMobileControllerBase
    {
        private readonly ICategoryQueryService _categoryQueryService;
        public CategoryWithProductController(ICategoryQueryService categoryQueryService)
        {
            _categoryQueryService = categoryQueryService;
        }
        public IHttpActionResult Get(Guid categoryRootId, Guid? brandId = null)
        {
            var dto = new MobileResultDto
            {
                Result =
                    _categoryQueryService.GetCategoriesWithProducts(categoryRootId, brandId)
            };
            return Ok(dto);
        }
    }
}