using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Persons.Commands.Shop;
using Shopping.Commands.Commands.Persons.Responses;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.Shops
{
    public class ActiveShopController : ApiControllerBase
    {
        public ActiveShopController(ICommandBus bus) : base(bus)
        {
        }
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(ActiveShopCommand command)
        {
            var response = await Bus.Send<ActiveShopCommand, ActiveShopCommandResponse>(command);
            return Ok(response);
        }
    }
}