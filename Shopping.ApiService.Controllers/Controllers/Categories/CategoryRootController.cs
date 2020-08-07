using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Categories.Commands;
using Shopping.Commands.Commands.Categories.Responses;
using Shopping.Infrastructure.Core.CommandBus;
using Shopping.QueryService.Interfaces.Categories;

namespace Shopping.ApiService.Controllers.Controllers.Categories
{
    [RoutePrefix("api/CategoryRoot")]
        [Authorize(Roles = "Support,Admin")]
    public class CategoryRootController : ApiControllerBase
    {
        private readonly ICategoryQueryService _categoryQueryService;
        public CategoryRootController(ICommandBus bus, ICategoryQueryService categoryQueryService) : base(bus)
        {
            _categoryQueryService = categoryQueryService;
        }
        /// <summary>
        /// دریافت دسته بندی های ریشه
        /// </summary>
        /// <returns></returns>
        public async Task<IHttpActionResult> Get()
        {
            return Ok(_categoryQueryService.GetCategoryRoots());
        }
        /// <summary>
        /// دریافت یک دسته اصلی
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(CreateCategoryRootCommand command)
        {
            var response = await
                Bus.Send<CreateCategoryRootCommand, CreateCategoryRootCommandResponse>(command);
            return Ok(response);
        }
        /// <summary>
        /// ویرایش یک دسته اصلی
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put(UpdateCategoryRootCommand command)
        {
            var response = await Bus.Send<UpdateCategoryRootCommand, UpdateCategoryRootCommandResponse>(command);
            return Ok(response);
        }
        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var command = new DeleteCategoryRootCommand
            {
                Id = id
            };
            var response = await Bus.Send<DeleteCategoryRootCommand, DeleteCategoryRootCommandResponse>(command);
            return Ok(response);
        }

        /// <summary>
        /// ویرایش order دسته روت
        /// </summary>
        /// <returns></returns>
        [Route("Sort")]
        public async Task<IHttpActionResult> Put(SortCategoryRootCommand command)
        {
            var response =
                await Bus.Send<SortCategoryRootCommand, SortCategoryRootCommandResponse>(command);
            return Ok(response);
        }
    }
}