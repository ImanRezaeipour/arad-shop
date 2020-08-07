using System.Web.Http;
using Shopping.Infrastructure.SeedWorks;
using Shopping.Mobile.ApiService.Controllers.SeedWorks;
using Shopping.QueryService.Interfaces.Categories;

#pragma warning disable 1591
namespace Shopping.Mobile.ApiService.Controllers.Controllers.Categories
{
    public class CategoryRootController : ApiMobileControllerBase
    {
        private readonly ICategoryQueryService _categoryQueryService;
        public CategoryRootController(ICategoryQueryService categoryQueryService)
        {
            _categoryQueryService = categoryQueryService;
        }
        public IHttpActionResult Get()
        {
            var dto = new MobileResultDto
            {
                Result = _categoryQueryService.GetActiveCategoryRoots()
            };
            return Ok(dto);
        }
    }
}