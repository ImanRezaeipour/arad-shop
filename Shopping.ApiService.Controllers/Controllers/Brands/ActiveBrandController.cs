using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Brands.Commands;
using Shopping.Commands.Commands.Brands.Responses;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.Brands
{
    public class ActiveBrandController : ApiControllerBase
    {
        public ActiveBrandController(ICommandBus bus) : base(bus)
        {
        }
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(ActiveBrandCommand command)
        {
            var response = await
                Bus.Send<ActiveBrandCommand, ActiveBrandCommandResponse>(command);
            return Ok(response);
        }
    }
}