using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Persons.Commands.Customer;
using Shopping.Commands.Commands.Persons.Responses;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.Customers
{
    public class ActiveCustomerController : ApiControllerBase
    {
        public ActiveCustomerController(ICommandBus bus) : base(bus)
        {

        }
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(ActiveCustomerCommand command)
        {
            var response = await Bus.Send<ActiveCustomerCommand, ActiveCustomerCommandResponse>(command);
            return Ok(response);
        }
    }
}