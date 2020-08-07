using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Brands.Commands;
using Shopping.Commands.Commands.Brands.Responses;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.Brands
{
    public class DeActiveBrandController : ApiControllerBase
    {
        public DeActiveBrandController(ICommandBus bus) : base(bus)
        {
        }
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(DeActiveBrandCommand command)
        {
            var response = await
                Bus.Send<DeActiveBrandCommand, DeActiveBrandCommandResponse>(command);
            return Ok(response);
        }
    }
}