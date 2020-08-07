using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Categories.Commands;
using Shopping.Commands.Commands.Categories.Responses;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.Categories
{
    public class DeActiveCategoryController : ApiControllerBase
    {
        public DeActiveCategoryController(ICommandBus bus) : base(bus)
        {
        }
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(DeActiveCategoryCommand command)
        {
            var response = await
                Bus.Send<DeActiveCategoryCommand, DeActiveCategoryCommandResponse>(command);
            return Ok(response);
        }
    }
}