using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.BaseEntities.Commands;
using Shopping.Commands.Commands.BaseEntities.Responses;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.BaseEntites
{
    [RoutePrefix("api/Zone")]
    [Authorize(Roles = "Support,Admin")]
    public class ZoneController : ApiControllerBase
    {
        public ZoneController(ICommandBus bus):base(bus)
        {
        }
        public async Task<IHttpActionResult> Post(CreateZoneCommand command)
        {
            var response = await
                Bus.Send<CreateZoneCommand, CreateZoneCommandResponse>(command);
            return Ok(response);
        }
        [Route("Update")]
        public async Task<IHttpActionResult> Put(UpdateZoneCommand command)
        {
            var response = await
                Bus.Send<UpdateZoneCommand, UpdateZoneCommandResponse>(command);
            return Ok(response);
        }
        [Route("DeActive")]
        public async Task<IHttpActionResult> Put(DeActiveZoneCommand command)
        {
            var response = await
                Bus.Send<DeActiveZoneCommand, DeActiveZoneCommandResponse>(command);
            return Ok(response);
        }
        [Route("Active")]
        public async Task<IHttpActionResult> Put(ActiveZoneCommand command)
        {
            var response = await
                Bus.Send<ActiveZoneCommand, ActiveZoneCommandResponse>(command);
            return Ok(response);
        }
    }
}