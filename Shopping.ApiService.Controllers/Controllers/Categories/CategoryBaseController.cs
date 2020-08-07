using System;
using System.Web.Http;
using Shopping.QueryService.Interfaces.Categories;

namespace Shopping.ApiService.Controllers.Controllers.Categories
{
    public class CategoryBaseController : ApiControllerBase
    {
        private readonly ICategoryQueryService _categoryQueryService;
        public CategoryBaseController(ICategoryQueryService categoryQueryService)
        {
            _categoryQueryService = categoryQueryService;
        }
        /// <summary>
        ///دریافت تمام دسته ها یا زیر دستهئهای خود
        /// </summary>
        /// <returns></returns>
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get()
        {
            var result = _categoryQueryService.GetAll();
            return Ok(result);
        }
        /// <summary>
        /// دریافت یک دسته
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = "Support,Admin")]
        public IHttpActionResult Get(Guid id)
        {
            return Ok(_categoryQueryService.GetCategoryById(id));
        }
    }
}