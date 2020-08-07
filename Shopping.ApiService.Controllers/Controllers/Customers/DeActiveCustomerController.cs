using System.Threading.Tasks;
using System.Web.Http;
using Shopping.Commands.Commands.Persons.Commands.Customer;
using Shopping.Commands.Commands.Persons.Responses;
using Shopping.Infrastructure.Core.CommandBus;

namespace Shopping.ApiService.Controllers.Controllers.Customers
{
    public class DeActiveCustomerController : ApiControllerBase
    {
        public DeActiveCustomerController(ICommandBus bus) : base(bus)
        {
        }
        [Authorize(Roles = "Support,Admin")]
        public async Task<IHttpActionResult> Put(DeActiveCustomerCommand command)
        {
            var response = await Bus.Send<DeActiveCustomerCommand, DeActiveCustomerCommandResponse>(command);
            return Ok(response);
        }
    }
}