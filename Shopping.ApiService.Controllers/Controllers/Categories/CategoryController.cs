using System;
using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Categories.Commands;
using Shopping.Commands.Commands.Categories.Responses;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.Categories
{
    [Authorize(Roles = "Support,Admin")]
    [RoutePrefix("api/Category")]
    public class CategoryController : ApiControllerBase
    {
        public CategoryController(ICommandBus bus) : base(bus)
        {
        }
        /// <summary>
        /// ایجاد یک زیر دسته
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Post(CreateCategoryCommand command)
        {
            var response = await
                Bus.Send<CreateCategoryCommand, CreateCategoryCommandResponse>(command);
            return Ok(response);
        }
        /// <summary>
        /// ویرایش یک زیر دسته
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        public async Task<IHttpActionResult> Put(UpdateCategoryCommand command)
        {
            var response = await Bus.Send<UpdateCategoryCommand, UpdateCategoryCommandResponse>(command);
            return Ok(response);
        }

        public async Task<IHttpActionResult> Delete(Guid id)
        {
            var command = new DeleteCategoryCommand
            {
                Id = id
            };
            var response = await Bus.Send<DeleteCategoryCommand, DeleteCategoryCommandResponse>(command);
            return Ok(response);
        }
        /// <summary>
        /// ویرایش order زیر دسته بندی
        /// </summary>
        /// <param name="command"></param>
        /// <returns></returns>
        [Route("Sort")]
        public async Task<IHttpActionResult> Put(SortSubCategoryCommand command)
        {
            var response =
                await Bus.Send<SortSubCategoryCommand, SortCategoryCommandResponse>(command);
            return Ok(response);
        }
    }
}