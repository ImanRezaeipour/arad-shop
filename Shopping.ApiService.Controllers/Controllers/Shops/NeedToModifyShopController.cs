using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Persons.Commands.Shop;
using Shopping.Commands.Commands.Persons.Responses;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.Shops
{
    public class NeedToModifyShopController:ApiControllerBase
    {
        public NeedToModifyShopController(ICommandBus bus):base(bus)
        {
            
        }
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(NeedToModifyShopCommand command)
        {
            command.UserId = UserId;
            command.FirstName = FirstName;
            command.LastName = LastName;
            var respose = await Bus.Send<NeedToModifyShopCommand, NeedToModifyShopCommandResponse>(command);
            return Ok(respose);
        }

    }
}