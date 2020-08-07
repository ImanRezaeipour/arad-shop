using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Categories.Commands;
using Shopping.Commands.Commands.Categories.Responses;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.Categories
{
    public class ActiveCategoryController : ApiControllerBase
    {
        public ActiveCategoryController(ICommandBus bus) : base(bus)
        {
        }
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(ActiveCategoryCommand command)
        {
            var response = await
                Bus.Send<ActiveCategoryCommand, ActiveCategoryCommandResponse>(command);
            return Ok(response);
        }
    }
}