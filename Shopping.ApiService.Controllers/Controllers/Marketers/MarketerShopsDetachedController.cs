using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Marketers.Commands;
using Shopping.Commands.Commands.Marketers.Responses;
using Shopping.Commands.Commands.Shared;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.Marketers
{
    [Authorize(Roles = "Support,Admin")]
    public class MarketerShopsDetachedController:ApiControllerBase
    {
        public MarketerShopsDetachedController(ICommandBus bus):base(bus)
        {
            
        }
       public async Task<IHttpActionResult> Put(DetachedMarketerShopCommand command)
        {
            command.UserInfo = new UserInfoCommandItem(UserId, FirstName, LastName);
            var response = await Bus.Send<DetachedMarketerShopCommand, MarketerCommandResponse>(command);
            return Ok(response);
        }

    }
}