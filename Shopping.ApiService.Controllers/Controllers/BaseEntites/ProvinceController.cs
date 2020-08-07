using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.BaseEntities.Commands;
using Shopping.Commands.Commands.BaseEntities.Responses;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.BaseEntites
{
    [Authorize(Roles = "Support,Admin.Operator")]
    public class ProvinceController:ApiControllerBase
    {
        public ProvinceController(ICommandBus bus):base(bus)
        {
        }
        public async Task<IHttpActionResult> Post(CreateProvinceCommand command)
        {
            var response = await
                Bus.Send<CreateProvinceCommand, CreateProvinceCommandResponse>(command);
            return Ok(response);
        }
        public async Task<IHttpActionResult> Put(UpdateProvinceCommand command)
        {
            var response = await
                Bus.Send<UpdateProvinceCommand, UpdateProvinceCommandResponse>(command);
            return Ok(response);
        }
    }
}